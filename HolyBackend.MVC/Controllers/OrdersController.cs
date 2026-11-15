using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HolyBackend.Core.Exceptions;
using HolyBackend.Core.Interfaces;
using HolyBackend.Core.Models;
using HolyBackend.Core.Patterns.Strategy;
using HolyBackend.DAL;
using HolyBackend.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HolyBackend.MVC.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly HolyDbContext _dbContext;

        public OrdersController(IOrderService orderService, HolyDbContext dbContext)
        {
            _orderService = orderService;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Cabinet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var isAdmin = User.IsInRole("Admin");

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return RedirectToAction("Login", "Account");

            if (await IsUserActivelyBannedAsync(user))
            {
                return RedirectToAction("Banned", "Account", new { userId = user.Id });
            }

            var orders = isAdmin
                ? await _orderService.GetAllOrdersAsync()
                : await _orderService.GetClientOrdersAsync(userId);

            var rankContext = new ClientRankContext();
            ViewBag.UserName = user.UserName;
            ViewBag.UserEmail = user.Email;
            ViewBag.CurrentUserId = userId;
            ViewBag.IsAdmin = isAdmin;
            ViewBag.ClientTitle = isAdmin && !string.IsNullOrEmpty(user.CustomTitle)
                ? user.CustomTitle
                : (user.IsHolySponsor ? "Святий Спонсор" : rankContext.CalculateTitle(user.CompletedOrdersCount));
            ViewBag.CompletedCount = user.CompletedOrdersCount;

            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (User.IsInRole("Admin")) return RedirectToAction(nameof(Cabinet));

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null && await IsUserActivelyBannedAsync(user))
            {
                return RedirectToAction("Banned", "Account", new { userId = user.Id });
            }

            return View(new CreateOrderViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderViewModel model)
        {
            if (User.IsInRole("Admin")) return RedirectToAction(nameof(Cabinet));
            if (!ModelState.IsValid) return View(model);

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user != null && await IsUserActivelyBannedAsync(user))
                {
                    return RedirectToAction("Banned", "Account", new { userId = user.Id });
                }

                var createdOrder = await _orderService.CreateOrderAsync(
                    model.Title,
                    model.Description,
                    model.Type,
                    userId
                );

                TempData["SuccessMessage"] = $"✨ Обряд «{createdOrder.Title}» успішно створено та занесено у Гримуар!";
                return RedirectToAction(nameof(Details), new { id = createdOrder.Id });
            }
            catch (DomainException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var isAdmin = User.IsInRole("Admin");

                if (!isAdmin && order.ClientId != currentUserId) return Forbid();

                var model = new CreateOrderViewModel
                {
                    Id = order.Id,
                    Title = order.Title,
                    Description = order.Description,
                    Type = order.Type
                };

                return View(model);
            }
            catch (OrderNotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Cabinet));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateOrderViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var isAdmin = User.IsInRole("Admin");

                await _orderService.UpdateOrderAsync(
                    model.Id,
                    model.Title,
                    model.Description,
                    model.Type,
                    currentUserId,
                    isAdmin
                );

                TempData["SuccessMessage"] = $"✏️ Параметри замовлення «{model.Title}» успішно відредаговано!";
                return RedirectToAction(nameof(Details), new { id = model.Id });
            }
            catch (DomainException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var isAdmin = User.IsInRole("Admin");
                await _orderService.DeleteOrderAsync(id, currentUserId, isAdmin);
                TempData["SuccessMessage"] = "🗑️ Замовлення та сувої його чату успішно видалено з архівів.";
            }
            catch (DomainException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Cabinet));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == currentUserId);
                if (user != null && await IsUserActivelyBannedAsync(user))
                {
                    return RedirectToAction("Banned", "Account", new { userId = user.Id });
                }

                var order = await _orderService.GetOrderByIdAsync(id);
                var isAdmin = User.IsInRole("Admin");

                if (!isAdmin && order.ClientId != currentUserId) return Forbid();

                var unreadMessages = await _dbContext.OrderMessages
                    .Where(m => m.OrderId == id && m.SenderId != currentUserId && !m.IsRead)
                    .ToListAsync();

                if (unreadMessages.Any())
                {
                    foreach (var msg in unreadMessages)
                    {
                        msg.IsRead = true;
                    }
                    await _dbContext.SaveChangesAsync();
                }

                ViewBag.IsAdmin = isAdmin;
                ViewBag.CurrentUserId = currentUserId;
                return View(order);
            }
            catch (OrderNotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Cabinet));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMessage(Guid orderId, string messageText)
        {
            if (string.IsNullOrWhiteSpace(messageText))
                return RedirectToAction(nameof(Details), new { id = orderId });

            try
            {
                var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == senderId);
                if (user != null && await IsUserActivelyBannedAsync(user))
                {
                    return RedirectToAction("Banned", "Account", new { userId = user.Id });
                }

                await _orderService.AddMessageToOrderAsync(orderId, senderId, messageText);
                TempData["SuccessMessage"] = "🕯️ Повідомлення успішно передано в Кімнату Сповіді.";
            }
            catch (DomainException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Details), new { id = orderId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMessage(Guid messageId, Guid orderId, string newText)
        {
            if (!string.IsNullOrWhiteSpace(newText))
            {
                try
                {
                    var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                    var isAdmin = User.IsInRole("Admin");
                    await _orderService.UpdateMessageAsync(messageId, newText, currentUserId, isAdmin);
                    TempData["SuccessMessage"] = "✏️ Повідомлення в чаті успішно відредаговано.";
                }
                catch (DomainException ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
            }
            return RedirectToAction(nameof(Details), new { id = orderId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMessage(Guid messageId, Guid orderId)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var isAdmin = User.IsInRole("Admin");
                await _orderService.DeleteMessageAsync(messageId, currentUserId, isAdmin);
                TempData["SuccessMessage"] = "🗑️ Повідомлення видалено з Кімнати Сповіді.";
            }
            catch (DomainException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Details), new { id = orderId });
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesJson(Guid id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var isAdmin = User.IsInRole("Admin");

            var unread = await _dbContext.OrderMessages
                .Where(m => m.OrderId == id && m.SenderId != currentUserId && !m.IsRead)
                .ToListAsync();
            if (unread.Any())
            {
                unread.ForEach(m => m.IsRead = true);
                await _dbContext.SaveChangesAsync();
            }

            var order = await _orderService.GetOrderByIdAsync(id);

            var result = order.Messages.Select(m => new
            {
                id = m.Id,
                senderName = m.Sender?.UserName ?? "Невідомий",
                senderRole = m.Sender?.Role ?? "Client",
                senderTitle = !string.IsNullOrEmpty(m.Sender?.CustomTitle)
                    ? m.Sender.CustomTitle
                    : (m.Sender?.Role == "Admin" ? "Інквізитор" : "Парафіянин"),
                text = m.Text,
                sentAt = m.SentAt.ToLocalTime().ToString("HH:mm dd.MM"),
                canManage = isAdmin || m.SenderId == currentUserId
            });
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUnreadNotifications()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var isAdmin = User.IsInRole("Admin");

            var query = _dbContext.OrderMessages
                .Include(m => m.Order)
                .Include(m => m.Sender)
                .Where(m => !m.IsRead && m.SenderId != currentUserId);

            if (!isAdmin)
            {
                query = query.Where(m => m.Order.ClientId == currentUserId);
            }

            var unreadList = await query
                .OrderByDescending(m => m.SentAt)
                .ToListAsync();

            var groupedByOrder = unreadList
                .GroupBy(m => new { m.OrderId, m.Order.Title })
                .Select(g => new
                {
                    orderId = g.Key.OrderId,
                    orderCode = "#PD-" + g.Key.OrderId.ToString().Substring(0, 6).ToUpper(),
                    orderTitle = g.Key.Title,
                    unreadCount = g.Count(),
                    lastSender = g.First().Sender?.UserName ?? "Співрозмовник"
                })
                .ToList();

            return Json(new
            {
                totalUnread = unreadList.Count,
                chats = groupedByOrder
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(Guid orderId, OrderStatus newStatus)
        {
            try
            {
                await _orderService.ChangeOrderStatusAsync(orderId, newStatus);
                TempData["SuccessMessage"] = $"⚔️ Статус замовлення змінено на «{newStatus}»!";
            }
            catch (DomainException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Details), new { id = orderId });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Grimoire()
        {
            var users = await _dbContext.Users.ToListAsync();
            var allOrders = await _dbContext.Orders.ToListAsync();
            var rankContext = new ClientRankContext();

            var stats = users.Select(u =>
            {
                var userOrders = allOrders.Where(o => o.ClientId == u.Id).ToList();
                return new ClientCrmViewModel
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    Role = u.Role,
                    RankTitle = u.Role == "Admin"
                        ? u.CustomTitle
                        : (u.IsHolySponsor ? "Святий Спонсор" : rankContext.CalculateTitle(u.CompletedOrdersCount)),
                    FirstContactDate = userOrders.Any() ? userOrders.Min(o => o.CreatedAt) : u.RegisteredAt,
                    TotalOrders = userOrders.Count,
                    InProgressOrders = userOrders.Count(o => o.Status == OrderStatus.InProgress || o.Status == OrderStatus.Pending),
                    CompletedOrders = u.CompletedOrdersCount,
                    IsBanned = u.IsBanned,
                    BanReason = u.BanReason,
                    BannedUntil = u.BannedUntil,
                    IsHolySponsor = u.IsHolySponsor
                };
            }).ToList();

            return View(stats);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleBanUser(string userId, string? banReason, int? durationHours)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null && user.Role != "Admin")
            {
                if (user.IsBanned)
                {
                    user.IsBanned = false;
                    user.BanReason = null;
                    user.BannedUntil = null;
                    TempData["SuccessMessage"] = $"🕊️ З учасника «{user.UserName}» знято Анафему. Доступ відновлено!";
                }
                else
                {
                    user.IsBanned = true;
                    user.BanReason = string.IsNullOrWhiteSpace(banReason)
                        ? "Порушення священних канонів Ордену Багоугодних"
                        : banReason.Trim();

                    user.BannedUntil = (durationHours.HasValue && durationHours.Value > 0)
                        ? DateTime.UtcNow.AddHours(durationHours.Value)
                        : null;     

                    string durationText = user.BannedUntil.HasValue
                        ? $"до {user.BannedUntil.Value.ToLocalTime():dd.MM.yyyy HH:mm}"
                        : "ДОВІЧНО";

                    TempData["SuccessMessage"] = $"🔥 На учасника «{user.UserName}» накладено Анафему ({durationText})!";
                }

                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Grimoire));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null && user.Role != "Admin")
            {
                var name = user.UserName;
                var userMessages = await _dbContext.OrderMessages.Where(m => m.SenderId == userId).ToListAsync();
                _dbContext.OrderMessages.RemoveRange(userMessages);

                var userOrders = await _dbContext.Orders.Where(o => o.ClientId == userId).ToListAsync();
                _dbContext.Orders.RemoveRange(userOrders);

                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();

                TempData["SuccessMessage"] = $"🗑️ Учасника «{name}» та всі його записи назавжди видалено з Гримуару.";
            }
            return RedirectToAction(nameof(Grimoire));
        }

        private async Task<bool> IsUserActivelyBannedAsync(ApplicationUser user)
        {
            if (!user.IsBanned) return false;

            if (user.BannedUntil.HasValue && user.BannedUntil.Value <= DateTime.UtcNow)
            {
                user.IsBanned = false;
                user.BanReason = null;
                user.BannedUntil = null;
                await _dbContext.SaveChangesAsync();
                return false;
            }

            return true;
        }
    }
}