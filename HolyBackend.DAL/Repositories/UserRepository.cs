using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HolyBackend.Core.Interfaces;
using HolyBackend.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace HolyBackend.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HolyDbContext _context;

        public UserRepository(HolyDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var lower = email.ToLowerInvariant();
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == lower, cancellationToken);
        }

        public async Task<ApplicationUser?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            var lower = userName.ToLowerInvariant();
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == lower, cancellationToken);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Users.OrderByDescending(u => u.RegisteredAt).ToListAsync(cancellationToken);
        }

        public async Task AddAsync(ApplicationUser user, CancellationToken cancellationToken = default)
        {
            await _context.Users.AddAsync(user, cancellationToken);
        }

        public void Update(ApplicationUser user)
        {
            _context.Users.Update(user);
        }

        public void Delete(ApplicationUser user)
        {
            var userMessages = _context.OrderMessages.Where(m => m.SenderId == user.Id);
            _context.OrderMessages.RemoveRange(userMessages);
            _context.Users.Remove(user);
        }
    }
}