using System;
using System.Threading.Tasks;
using HolyBackend.Core.Models;

namespace HolyBackend.Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string title, string description, OrderType type, string clientId);
        Task ChangeOrderStatusAsync(Guid orderId, OrderStatus newStatus);
        Task AddMessageToOrderAsync(Guid orderId, string senderId, string text);
    }
}