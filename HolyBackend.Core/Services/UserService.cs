using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using HolyBackend.Core.Exceptions;
using HolyBackend.Core.Interfaces;
using HolyBackend.Core.Models;

namespace HolyBackend.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<ApplicationUser> _userValidator;

        public UserService(IUnitOfWork unitOfWork, IValidator<ApplicationUser> userValidator)
        {
            _unitOfWork = unitOfWork;
            _userValidator = userValidator;
        }

        public async Task<ApplicationUser?> GetByIdAsync(string userId)
        {
            return await _unitOfWork.Users.GetByIdAsync(userId);
        }

        public async Task UpdateProfileAsync(string userId, string newUserName, string newEmail)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId)
                ?? throw new InvalidOrderStateException("Профіль користувача не знайдено.");

            var normalizedName = newUserName.Trim();
            var normalizedEmail = newEmail.Trim().ToLowerInvariant();

            var existingByEmail = await _unitOfWork.Users.GetByEmailAsync(normalizedEmail);
            if (existingByEmail != null && existingByEmail.Id != userId)
            {
                throw new InvalidOrderStateException("Ця електронна пошта вже закріплена за іншим учасником Ордену.");
            }

            var existingByName = await _unitOfWork.Users.GetByUserNameAsync(normalizedName);
            if (existingByName != null && existingByName.Id != userId)
            {
                throw new InvalidOrderStateException("Це ім'я вже зайняте іншим братом чи сестрою Ордену.");
            }

            user.UserName = normalizedName;
            user.Email = normalizedEmail;

            var validationResult = await _userValidator.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                throw new OrderValidationException(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId)
                ?? throw new InvalidOrderStateException("Профіль користувача не знайдено.");

            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
            {
                throw new InvalidOrderStateException("Новий таємний пароль має містити щонайменше 6 символів.");
            }

            var currentHash = HashPassword(currentPassword);
            if (user.PasswordHash != currentHash)
            {
                throw new InvalidOrderStateException("Поточний пароль введено невірно.");
            }

            var newHash = HashPassword(newPassword);
            if (user.PasswordHash == newHash)
            {
                throw new InvalidOrderStateException("Новий пароль має відрізнятися від старого.");
            }

            user.PasswordHash = newHash;
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(string userId, string confirmPassword)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId)
                ?? throw new InvalidOrderStateException("Профіль користувача не знайдено.");

            var hash = HashPassword(confirmPassword);
            if (user.PasswordHash != hash)
            {
                throw new InvalidOrderStateException("Невірний пароль підтвердження. Видалення профілю скасовано.");
            }

            if (user.Role == "Admin")
            {
                var allUsers = await _unitOfWork.Users.GetAllAsync();
                if (allUsers.Count(u => u.Role == "Admin") <= 1)
                {
                    throw new InvalidOrderStateException("Неможливо видалити останнього Верховного Інквізитора Ордену!");
                }
            }

            var clientOrders = await _unitOfWork.Orders.GetByClientIdAsync(userId);
            foreach (var order in clientOrders)
            {
                _unitOfWork.Orders.Delete(order);
            }

            _unitOfWork.Users.Delete(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public string HashPassword(string rawPassword)
        {
            return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(rawPassword)));
        }
    }
}