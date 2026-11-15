using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HolyBackend.Core.Models;
using HolyBackend.Core.Patterns.Strategy;
using HolyBackend.DAL;
using HolyBackend.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HolyBackend.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HolyDbContext _db;

        public HomeController(HolyDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult About() => View();

        [HttpGet]
        public IActionResult Services() => View();

        [HttpGet]
        public IActionResult Portfolio() => View();

        [HttpGet]
        public async Task<IActionResult> Statistics(string period = "all")
        {
            var query = _db.Orders.AsQueryable();
            var now = DateTime.UtcNow;
            string periodLabel = "За весь час існування Ордену";

            switch (period)
            {
                case "7days":
                    query = query.Where(o => o.CreatedAt >= now.AddDays(-7));
                    periodLabel = "За останні 7 днів";
                    break;
                case "30days":
                    query = query.Where(o => o.CreatedAt >= now.AddDays(-30));
                    periodLabel = "За останні 30 днів";
                    break;
                case "year":
                    query = query.Where(o => o.CreatedAt >= now.AddYears(-1));
                    periodLabel = "За останній рік";
                    break;
                default:
                    period = "all";
                    break;
            }

            var orders = await query.ToListAsync();

            var model = new OrderStatisticsViewModel
            {
                SelectedPeriod = period,
                PeriodLabel = periodLabel,
                TotalOrders = orders.Count,
                CompletedOrders = orders.Count(o => o.Status == OrderStatus.Completed),
                InProgressOrders = orders.Count(o => o.Status == OrderStatus.InProgress),
                PendingOrders = orders.Count(o => o.Status == OrderStatus.Pending),
                CancelledOrders = orders.Count(o => o.Status == OrderStatus.Cancelled),
                OrdersByType = Enum.GetValues<OrderType>()
                    .ToDictionary(
                        t => t.ToString(),
                        t => orders.Count(o => o.Type == t)
                    )
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Sponsors()
        {
            var allOrders = await _db.Orders.ToListAsync();

            var sponsors = await _db.Users
                .Where(u => u.Role != "Admin" && !u.IsBanned && (u.IsHolySponsor || u.CompletedOrdersCount >= 5))
                .OrderByDescending(u => u.CompletedOrdersCount)
                .ThenBy(u => u.RegisteredAt)
                .ToListAsync();

            var model = sponsors.Select(u => new HolySponsorViewModel
            {
                UserId = u.Id,
                UserName = u.UserName,
                CustomTitle = u.CompletedOrdersCount >= 5 ? "Магістр Ордену • Святий Спонсор" : "Почесний Святий Спонсор",
                CompletedOrdersCount = u.CompletedOrdersCount,
                TotalOrdersCount = allOrders.Count(o => o.ClientId == u.Id),
                JoinedAt = u.RegisteredAt,
                IsManualSponsor = u.IsHolySponsor
            }).ToList();

            if (User.IsInRole("Admin"))
            {
                ViewBag.Candidates = await _db.Users
                    .Where(u => u.Role != "Admin" && !u.IsBanned && !u.IsHolySponsor && u.CompletedOrdersCount < 5)
                    .ToListAsync();
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleSponsorStatus(string userId, string returnUrl = "Sponsors")
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null && user.Role != "Admin")
            {
                user.IsHolySponsor = !user.IsHolySponsor;
                if (user.IsHolySponsor)
                {
                    user.CustomTitle = "Святий Спонсор";
                }
                else
                {
                    user.CustomTitle = new ClientRankContext().CalculateTitle(user.CompletedOrdersCount);
                }
                await _db.SaveChangesAsync();
            }

            if (returnUrl == "Grimoire")
                return RedirectToAction("Grimoire", "Orders");
            if (user != null && user.Role != "Admin")
            {
                TempData["SuccessMessage"] = user.IsHolySponsor
                    ? $"👑 Учасника «{user.UserName}» успішно піднесено до Пантеону Святих Спонсорів!"
                    : $"Почесний статус Святого Спонсора знято з «{user.UserName}».";
            }

            return RedirectToAction(nameof(Sponsors));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSponsorDirectly(string userName, string email, int completedProjects = 5)
        {
            if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(email))
            {
                var exists = await _db.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
                if (!exists)
                {
                    var hash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes("Patron777!")));
                    var newSponsor = new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = userName,
                        Email = email.ToLower(),
                        PasswordHash = hash,
                        Role = "Client",
                        CompletedOrdersCount = Math.Max(0, completedProjects),
                        IsHolySponsor = true,
                        CustomTitle = "Святий Спонсор",
                        RegisteredAt = DateTime.UtcNow
                    };

                    _db.Users.Add(newSponsor);
                    await _db.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"👑 Нового мецената «{userName}» успішно вписано в Пантеон Святих Спонсорів!";
                }
            }
            return RedirectToAction(nameof(Sponsors));
        }
    }
}