using System;
using System.Collections.Generic;

namespace HolyBackend.Core.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public OrderType Type { get; set; } = OrderType.Standard;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string ClientId { get; set; }
        public ApplicationUser Client { get; set; }

        public ICollection<OrderMessage> Messages { get; set; } = new List<OrderMessage>();
    }
}