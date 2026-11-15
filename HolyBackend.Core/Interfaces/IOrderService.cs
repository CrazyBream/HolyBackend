using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HolyBackend.Core.Models;

namespace HolyBackend.Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string title, string description, OrderType type, string clientId);
        Task<Order> GetOrderByIdAsync(Guid orderId);
        Task<IEnumerable<Order>> GetClientOrdersAsync(string clientId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task ChangeOrderStatusAsync(Guid orderId, OrderStatus newStatus);

        Task UpdateOrderAsync(Guid orderId, string newTitle, string newDescription, OrderType newType, string currentUserId, bool isAdmin);
        Task DeleteOrderAsync(Guid orderId, string currentUserId, bool isAdmin);

        Task AddMessageToOrderAsync(Guid orderId, string senderId, string text);
        Task UpdateMessageAsync(Guid messageId, string newText, string currentUserId, bool isAdmin);
        Task DeleteMessageAsync(Guid messageId, string currentUserId, bool isAdmin);
    }
}