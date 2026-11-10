using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace HolyBackend.Core.Models
{
    public class OrderMessage
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        public string Text { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}