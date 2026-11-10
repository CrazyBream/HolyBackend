using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HolyBackend.Core.Exceptions;
using HolyBackend.Core.Interfaces;
using HolyBackend.Core.Models;
using HolyBackend.Core.Patterns.Factory;
using HolyBackend.Core.Patterns.Observer;

namespace HolyBackend.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderFactory _orderFactory;
        private readonly IEnumerable<IOrderObserver> _observers;

        public OrderService(
            IUnitOfWork unitOfWork,
            IOrderFactory orderFactory,
            IEnumerable<IOrderObserver> observers) 
        {
            _unitOfWork = unitOfWork;
            _orderFactory = orderFactory;
            _observers = observers;
        }


        public async Task<Order> CreateOrderAsync(string title, string description, OrderType type, string clientId)
        {
            var order = _orderFactory.CreateOrder(title, description, type, clientId);

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            foreach (var observer in _observers)
            {
                await observer.HandleOrderCreatedAsync(order);
            }

            return order;
        }

        public async Task ChangeOrderStatusAsync(Guid orderId, OrderStatus newStatus)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId)
                ?? throw new OrderNotFoundException(orderId);

            if (order.Status == OrderStatus.Completed || order.Status == OrderStatus.Cancelled)
            {
                throw new InvalidOrderStateException("Неможливо змінити статус вже закритої заявки.");
            }

            order.Status = newStatus;

            _unitOfWork.Orders.Update(order);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddMessageToOrderAsync(Guid orderId, string senderId, string text)
        {
            var order = await _unitOfWork.Orders.GetByIdWithMessagesAsync(orderId)
                ?? throw new OrderNotFoundException(orderId);

            if (order.Status == OrderStatus.Completed || order.Status == OrderStatus.Cancelled)
            {
                throw new InvalidOrderStateException("Чат недоступний. Заявка вже виконана або скасована.");
            }

            var message = new OrderMessage
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                SenderId = senderId,
                Text = text,
                SentAt = DateTime.UtcNow
            };

            order.Messages.Add(message);
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}