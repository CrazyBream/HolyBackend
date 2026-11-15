using System;
using System.Threading.Tasks;
using HolyBackend.Core.Models;

namespace HolyBackend.Core.Patterns.Observer
{
    public class AuditLogObserver : IOrderObserver
    {
        public Task HandleOrderCreatedAsync(Order order)
        {
            Console.WriteLine($"[AuditObserver] Створено заявку '{order.Title}' ({order.Type}) о {order.CreatedAt:dd.MM.yyyy HH:mm}");
            return Task.CompletedTask;
        }

        public Task HandleOrderStatusChangedAsync(Order order, OrderStatus oldStatus, OrderStatus newStatus)
        {
            Console.WriteLine($"[AuditObserver] Статус заявки {order.Id} змінено: {oldStatus} -> {newStatus}");
            return Task.CompletedTask;
        }

        public Task HandleUserRankUpgradedAsync(ApplicationUser user, string oldTitle, string newTitle)
        {
            Console.WriteLine($"[AuditObserver] Парафіянин '{user.UserName}' отримав новий титул: '{oldTitle}' -> '{newTitle}'!");
            return Task.CompletedTask;
        }
    }
}