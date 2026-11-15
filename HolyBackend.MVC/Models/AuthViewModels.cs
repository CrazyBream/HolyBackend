using System;
using System.ComponentModel.DataAnnotations;

namespace HolyBackend.MVC.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Вкажіть ваше ім'я або назву компанії.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Ім'я має містити від 3 до 50 символів.")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯіІїЇєЄґҐ0-9\s\-_'.]+$", ErrorMessage = "Ім'я містить недопустимі символи.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле Email є обов'язковим.")]
        [EmailAddress(ErrorMessage = "Введіть коректну адресу електронної пошти (наприклад, name@domain.com).")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть таємний пароль.")]
        [MinLength(6, ErrorMessage = "Пароль має містити щонайменше 6 символів.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Підтвердіть ваш пароль.")]
        [Compare("Password", ErrorMessage = "Паролі не збігаються.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введіть ваш Email.")]
        [EmailAddress(ErrorMessage = "Некоректний формат Email.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть ваш пароль.")]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; } = false;
    }

    public class BanDetailsViewModel
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Reason { get; set; } = "Порушення священних канонів Ордену";
        public DateTime? BannedUntil { get; set; }
    }
    public class ProfileSettingsViewModel
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string CustomTitle { get; set; } = string.Empty;
        public int CompletedOrdersCount { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}