using HolyBackend.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyBackend.Core.Patterns.Factory
{
    public interface IOrderFactory
    {
        Order CreateOrder(string title, string description, OrderType type, string clientId);
    }
}
