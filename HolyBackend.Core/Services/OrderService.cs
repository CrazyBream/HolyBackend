using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using HolyBackend.Core.Exceptions;
using HolyBackend.Core.Interfaces;
using HolyBackend.Core.Models;
using HolyBackend.Core.Patterns.Factory;
using HolyBackend.Core.Patterns.Observer;
using HolyBackend.Core.Patterns.Strategy;

namespace HolyBackend.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderFactory _orderFactory;
        private readonly ClientRankContext _rankContext;
        private readonly IEnumerable<IOrderObserver> _observers;
        private readonly IValidator<Order> _orderValidator;
        private readonly IValidator<OrderMessage> _messageValidator;

        public OrderService(
            IUnitOfWork unitOfWork,
            IOrderFactory orderFactory,
            ClientRankContext rankContext,
            IEnumerable<IOrderObserver> observers,
            IValidator<Order> orderValidator,
            IValidator<OrderMessage> messageValidator)
        {
            _unitOfWork = unitOfWork;
            _orderFactory = orderFactory;
            _rankContext = rankContext;
            _observers = observers;
            _orderValidator = orderValidator;
            _messageValidator = messageValidator;
        }

        public async Task<Order> CreateOrderAsync(string title, string description, OrderType type, string clientId)
        {
            var order = _orderFactory.CreateOrder(title, description, type, clientId);

            var validationResult = await _orderValidator.ValidateAsync(order);
            if (!validationResult.IsValid)
            {
                throw new OrderValidationException(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            foreach (var observer in _observers)
            {
                await observer.HandleOrderCreatedAsync(order);
            }

            return order;
        }

        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            return await _unitOfWork.Orders.GetByIdWithMessagesAsync(orderId)
                ?? throw new OrderNotFoundException(orderId);
        }

        public async Task<IEnumerable<Order>> GetClientOrdersAsync(string clientId)
        {
            return await _unitOfWork.Orders.GetByClientIdAsync(clientId);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _unitOfWork.Orders.GetAllAsync();
        }

        public async Task UpdateOrderAsync(Guid orderId, string newTitle, string newDescription, OrderType newType, string currentUserId, bool isAdmin)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId)
                ?? throw new OrderNotFoundException(orderId);

            if (!isAdmin && order.ClientId != currentUserId)
            {
                throw new InvalidOrderStateException("Ви можете редагувати лише власні заявки.");
            }

            order.Title = _orderFactory.FormatTitleByType(newTitle, newType);
            order.Description = newDescription;
            order.Type = newType;

            var validationResult = await _orderValidator.ValidateAsync(order);
            if (!validationResult.IsValid)
            {
                throw new OrderValidationException(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            _unitOfWork.Orders.Update(order);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(Guid orderId, string currentUserId, bool isAdmin)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId)
                ?? throw new OrderNotFoundException(orderId);

            if (!isAdmin && order.ClientId != currentUserId)
            {
                throw new InvalidOrderStateException("Ви можете видаляти лише власні заявки.");
            }

            _unitOfWork.Orders.Delete(order);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ChangeOrderStatusAsync(Guid orderId, OrderStatus newStatus)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId)
                ?? throw new OrderNotFoundException(orderId);

            if (order.Status == OrderStatus.Completed || order.Status == OrderStatus.Cancelled)
            {
                throw new InvalidOrderStateException("Неможливо змінити статус вже закритої заявки.");
            }

            if (order.Status == OrderStatus.Pending && newStatus == OrderStatus.Completed)
            {
                throw new InvalidOrderStateException("Спочатку переведіть обряд у статус «InProgress (В роботі)», перш ніж завершувати його.");
            }

            var oldStatus = order.Status;
            order.Status = newStatus;
            _unitOfWork.Orders.Update(order);

            if (newStatus == OrderStatus.Completed)
            {
                var client = await _unitOfWork.Users.GetByIdAsync(order.ClientId);
                if (client != null)
                {
                    var oldTitle = client.CustomTitle;
                    client.CompletedOrdersCount++;

                    if (client.CompletedOrdersCount >= 5)
                    {
                        client.IsHolySponsor = true;
                    }

                    if (client.Role != "Admin")
                    {
                        client.CustomTitle = _rankContext.CalculateTitle(client.CompletedOrdersCount, client.IsHolySponsor);
                    }

                    _unitOfWork.Users.Update(client);

                    if (oldTitle != client.CustomTitle)
                    {
                        foreach (var observer in _observers)
                        {
                            await observer.HandleUserRankUpgradedAsync(client, oldTitle, client.CustomTitle);
                        }
                    }
                }
            }

            await _unitOfWork.SaveChangesAsync();

            foreach (var observer in _observers)
            {
                await observer.HandleOrderStatusChangedAsync(order, oldStatus, newStatus);
            }
        }

        public async Task AddMessageToOrderAsync(Guid orderId, string senderId, string text)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId)
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

            var validationResult = await _messageValidator.ValidateAsync(message);
            if (!validationResult.IsValid)
            {
                throw new OrderValidationException(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            await _unitOfWork.Orders.AddMessageAsync(message);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateMessageAsync(Guid messageId, string newText, string currentUserId, bool isAdmin)
        {
            var message = await _unitOfWork.Orders.GetMessageByIdAsync(messageId)
                ?? throw new InvalidOrderStateException("Повідомлення не знайдено.");

            if (!isAdmin && message.SenderId != currentUserId)
            {
                throw new InvalidOrderStateException("Ви можете редагувати лише власні повідомлення.");
            }

            message.Text = newText;

            var validationResult = await _messageValidator.ValidateAsync(message);
            if (!validationResult.IsValid)
            {
                throw new OrderValidationException(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            _unitOfWork.Orders.UpdateMessage(message);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteMessageAsync(Guid messageId, string currentUserId, bool isAdmin)
        {
            var message = await _unitOfWork.Orders.GetMessageByIdAsync(messageId)
                ?? throw new InvalidOrderStateException("Повідомлення не знайдено.");

            if (!isAdmin && message.SenderId != currentUserId)
            {
                throw new InvalidOrderStateException("Ви можете видаляти лише власні повідомлення.");
            }

            _unitOfWork.Orders.DeleteMessage(message);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}