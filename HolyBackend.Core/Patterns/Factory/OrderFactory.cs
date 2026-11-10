using HolyBackend.Core.Models;
using System;

namespace HolyBackend.Core.Patterns.Factory
{
    public class OrderFactory : IOrderFactory
    {
        public Order CreateOrder(string title, string description, OrderType type, string clientId)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                Type = type,
                ClientId = clientId,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Pending
            };

            switch (type)
            {
                case OrderType.Refactoring:
                    order.Title = $"[Терміново] Рефакторинг: {title}";
                    break;
                case OrderType.Design:
                    order.Title = $"[UI/UX] {title}";
                    break;
            }

            return order;
        }
    }
}