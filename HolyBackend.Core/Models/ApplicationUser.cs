using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyBackend.Core.Models
{
    public class ApplicationUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string CustomTitle { get; set; } = "Новачок";
        public int CompletedOrdersCount { get; set; }
    }
}
