using System.Threading;
using System.Threading.Tasks;

namespace HolyBackend.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IOrderRepository Orders { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}