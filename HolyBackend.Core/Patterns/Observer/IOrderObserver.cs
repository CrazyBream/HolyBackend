using System.Threading.Tasks;
using HolyBackend.Core.Models;

namespace HolyBackend.Core.Patterns.Observer
{
    public interface IOrderObserver
    {
        Task HandleOrderCreatedAsync(Order order);
        Task HandleOrderStatusChangedAsync(Order order, OrderStatus oldStatus, OrderStatus newStatus);
        Task HandleUserRankUpgradedAsync(ApplicationUser user, string oldTitle, string newTitle);
    }
}