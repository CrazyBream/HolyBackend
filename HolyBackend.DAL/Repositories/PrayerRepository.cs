using HolyBackend.Core.Interfaces;
using HolyBackend.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HolyBackend.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly HolyDbContext _context;

        public OrderRepository(HolyDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<Order?> GetByIdWithMessagesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Messages.OrderBy(m => m.SentAt))
                .ThenInclude(m => m.Sender)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetByClientIdAsync(string clientId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Where(o => o.ClientId == clientId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
        {
            await _context.Orders.AddAsync(order, cancellationToken);
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }

        public void Delete(Order order)
        {
            _context.Orders.Remove(order);
        }
    }
}