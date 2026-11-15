using HolyBackend.Core.Interfaces;
using HolyBackend.Core.Models;
using HolyBackend.DAL;
using HolyBackend.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HolyBackend.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly HolyDbContext _db;
        private readonly IUserService _userService;

        public AccountController(HolyDbContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register() => View(new RegisterViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var normalizedEmail = model.Email.Trim().ToLower();
            var normalizedName = model.UserName.Trim();

            if (await _db.Users.AnyAsync(u => u.Email.ToLower() == normalizedEmail))
            {
                ModelState.AddModelError("Email", "Парафіянин з такою електронною поштою вже зареєстрований у Гримуарі.");
                return View(model);
            }

            if (await _db.Users.AnyAsync(u => u.UserName.ToLower() == normalizedName.ToLower()))
            {
                ModelState.AddModelError("UserName", "Це ім'я вже використовується іншим братом Ордену. Оберіть інше.");
                return View(model);
            }

            try
            {
                var user = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = normalizedName,
                    Email = normalizedEmail,
                    PasswordHash = HashPassword(model.Password),
                    Role = "Client",
                    CustomTitle = "Новачок",
                    CompletedOrdersCount = 0,
                    RegisteredAt = DateTime.UtcNow
                };

                _db.Users.Add(user);
                await _db.SaveChangesAsync();

                await SignInUserAsync(user, isPersistent: true);
                TempData["SuccessMessage"] = $"🕯️ Вітаємо в Ордені, {user.UserName}! Вашу присягу прийнято.";
                return RedirectToAction("Cabinet", "Orders");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Сталася помилка зв'язку зі сховищем даних. Спробуйте пізніше.");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Login() => View(new LoginViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var normalizedEmail = model.Email.Trim().ToLower();
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail);

            if (user == null)
            {
                ModelState.AddModelError("Email", "Акаунт із такою поштою не знайдено в архівах Ордену.");
                return View(model);
            }

            var hash = HashPassword(model.Password);
            if (user.PasswordHash != hash)
            {
                ModelState.AddModelError("Password", "Невірний таємний пароль. Перевірте розкладку або натисніть іконку ока.");
                return View(model);
            }

            if (user.IsBanned)
            {
                if (user.BannedUntil.HasValue && user.BannedUntil.Value <= DateTime.UtcNow)
                {
                    user.IsBanned = false;
                    user.BanReason = null;
                    user.BannedUntil = null;
                    await _db.SaveChangesAsync();
                }
                else
                {
                    return RedirectToAction(nameof(Banned), new { userId = user.Id });
                }
            }

            await SignInUserAsync(user, model.RememberMe);
            TempData["SuccessMessage"] = $"🔑 З поверненням у Храм Коду, {user.UserName}!";
            return RedirectToAction("Cabinet", "Orders");
        }

        [HttpGet]
        public async Task<IActionResult> Banned(string userId)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null || !user.IsBanned)
            {
                return RedirectToAction(nameof(Login));
            }

            if (User.Identity?.IsAuthenticated == true)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }

            var model = new BanDetailsViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                Reason = string.IsNullOrWhiteSpace(user.BanReason)
                    ? "Єресь у коді та порушення канонів Ордену"
                    : user.BanReason,
                BannedUntil = user.BannedUntil
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["SuccessMessage"] = "🚪 Ви успішно покинули чертоги Кабінету.";
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUserAsync(ApplicationUser user, bool isPersistent)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                ExpiresUtc = isPersistent ? DateTimeOffset.UtcNow.AddDays(14) : DateTimeOffset.UtcNow.AddHours(2)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                authProperties);
        }
        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var user = await _userService.GetByIdAsync(userId);
            if (user == null) return RedirectToAction(nameof(Login));

            var model = new ProfileSettingsViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
                CustomTitle = user.CustomTitle,
                CompletedOrdersCount = user.CompletedOrdersCount,
                RegisteredAt = user.RegisteredAt
            };

            return View(model);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(string userName, string email)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            try
            {
                await _userService.UpdateProfileAsync(userId, userName, email);
                var updatedUser = await _userService.GetByIdAsync(userId);
                if (updatedUser != null)
                {
                    await SignInUserAsync(updatedUser, isPersistent: true);
                }
                TempData["SuccessMessage"] = "✨ Ваше ім'я та пошту успішно оновлено у Гримуарі!";
            }
            catch (HolyBackend.Core.Exceptions.DomainException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Profile));
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmNewPassword)
        {
            if (newPassword != confirmNewPassword)
            {
                TempData["ErrorMessage"] = "Новий пароль та його підтвердження не збігаються.";
                return RedirectToAction(nameof(Profile));
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            try
            {
                await _userService.ChangePasswordAsync(userId, currentPassword, newPassword);
                TempData["SuccessMessage"] = "🔑 Таємний пароль успішно змінено!";
            }
            catch (HolyBackend.Core.Exceptions.DomainException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Profile));
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMyAccount(string confirmPassword)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            try
            {
                await _userService.DeleteAccountAsync(userId, confirmPassword);
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                TempData["SuccessMessage"] = "🕯️ Ваш профіль та всі пов'язані сувої назавжди стерто з хронік Ордену.";
                return RedirectToAction("Index", "Home");
            }
            catch (HolyBackend.Core.Exceptions.DomainException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Profile));
            }
        }

        private static string HashPassword(string password)
        {
            return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
        }
    }
}