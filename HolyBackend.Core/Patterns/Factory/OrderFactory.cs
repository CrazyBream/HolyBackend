using System;
using HolyBackend.Core.Models;

namespace HolyBackend.Core.Patterns.Factory
{
    public class OrderFactory : IOrderFactory
    {
        public Order CreateOrder(string title, string description, OrderType type, string clientId)
        {
            return new Order
            {
                Id = Guid.NewGuid(),
                Title = FormatTitleByType(title, type),
                Description = description,
                Type = type,
                ClientId = clientId,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Pending
            };
        }

        public string FormatTitleByType(string title, OrderType type)
        {
            var cleanTitle = title
                .Replace("[Терміново] Рефакторинг: ", "")
                .Replace("[UI/UX] ", "")
                .Trim();

            return type switch
            {
                OrderType.Refactoring => $"[Терміново] Рефакторинг: {cleanTitle}",
                OrderType.Design => $"[UI/UX] {cleanTitle}",
                _ => cleanTitle
            };
        }
    }
}