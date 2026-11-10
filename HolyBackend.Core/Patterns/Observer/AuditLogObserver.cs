using HolyBackend.Core.Models;
using System;
using System.Threading.Tasks;

namespace HolyBackend.Core.Patterns.Observer
{
    public class AuditLogObserver : IOrderObserver
    {
        public Task HandleOrderCreatedAsync(Order order)
        {
            Console.WriteLine($"[AuditObserver] Запис у лог: Створено нову заявку типу {order.Type} о {order.CreatedAt}");
            return Task.CompletedTask;
        }
    }
}