using HolyBackend.Core.Models;
using System.Threading.Tasks;

namespace HolyBackend.Core.Patterns.Observer
{
    public interface IOrderObserver
    {
        Task HandleOrderCreatedAsync(Order order);
    }
}