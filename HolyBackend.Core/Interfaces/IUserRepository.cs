using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HolyBackend.Core.Models;

namespace HolyBackend.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<ApplicationUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<ApplicationUser?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
        Task<IEnumerable<ApplicationUser>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(ApplicationUser user, CancellationToken cancellationToken = default);
        void Update(ApplicationUser user);
        void Delete(ApplicationUser user);
    }
}