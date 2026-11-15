using System;
using System.Threading.Tasks;
using HolyBackend.Core.Models;

namespace HolyBackend.Core.Patterns.Observer
{
    public class EmailNotificationObserver : IOrderObserver
    {
        public Task HandleOrderCreatedAsync(Order order)
        {
            Console.WriteLine($"[EmailObserver] Лист клієнту {order.ClientId}: Заявка '{order.Title}' прийнята в роботу.");
            return Task.CompletedTask;
        }

        public Task HandleOrderStatusChangedAsync(Order order, OrderStatus oldStatus, OrderStatus newStatus)
        {
            Console.WriteLine($"[EmailObserver] Сповіщення клієнту {order.ClientId}: Статус '{order.Title}' змінено на {newStatus}.");
            return Task.CompletedTask;
        }

        public Task HandleUserRankUpgradedAsync(ApplicationUser user, string oldTitle, string newTitle)
        {
            Console.WriteLine($"[EmailObserver] Вітання для {user.Email}: Орден дарував вам титул «{newTitle}»!");
            return Task.CompletedTask;
        }
    }
}