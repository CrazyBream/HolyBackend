using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HolyBackend.Core.Models;

namespace HolyBackend.Core.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Order?> GetByIdWithMessagesAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Order>> GetByClientIdAsync(string clientId, CancellationToken cancellationToken = default);

        Task AddAsync(Order order, CancellationToken cancellationToken = default);
        void Update(Order order);
        void Delete(Order order);

        Task AddMessageAsync(OrderMessage message, CancellationToken cancellationToken = default);
        Task<OrderMessage?> GetMessageByIdAsync(Guid messageId, CancellationToken cancellationToken = default);
        void UpdateMessage(OrderMessage message);
        void DeleteMessage(OrderMessage message);
    }
}