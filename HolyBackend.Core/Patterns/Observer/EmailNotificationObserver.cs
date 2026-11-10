using HolyBackend.Core.Models;
using System;
using System.Threading.Tasks;

namespace HolyBackend.Core.Patterns.Observer
{
    public class EmailNotificationObserver : IOrderObserver
    {
        public Task HandleOrderCreatedAsync(Order order)
        {
            Console.WriteLine($"[EmailObserver] Відправлено лист клієнту {order.ClientId}: Заявка '{order.Title}' прийнята.");
            return Task.CompletedTask;
        }
    }
}