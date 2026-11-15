using HolyBackend.Core.Models;

namespace HolyBackend.Core.Patterns.Factory
{
    public interface IOrderFactory
    {
        Order CreateOrder(string title, string description, OrderType type, string clientId);
        string FormatTitleByType(string title, OrderType type);
    }
}