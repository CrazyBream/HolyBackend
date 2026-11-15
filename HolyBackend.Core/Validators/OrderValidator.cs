using System.Linq;
using FluentValidation;
using HolyBackend.Core.Models;

namespace HolyBackend.Core.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Назва заявки є обов'язковою.")
                .Length(5, 100).WithMessage("Назва має містити від 5 до 100 символів.")
                .Must(NotBeAllCaps).WithMessage("Не кричіть у Храмі Коду (вимкніть Caps Lock у назві).")
                .Must(HaveMeaningfulCharacters).WithMessage("Назва заявки має містити осмислений текст, а не набір однакових символів.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Опис проблеми є обов'язковим.")
                .MinimumLength(20).WithMessage("Опис має містити хоча б 20 символів для точного розуміння гріха.")
                .MaximumLength(2000).WithMessage("Сувій опису занадто довгий (максимум 2000 символів).");

            RuleFor(x => x.Description)
                .MinimumLength(35)
                .When(x => x.Type == OrderType.Refactoring)
                .WithMessage("Для обряду Рефакторингу опишіть симптоми бага детальніше (мінімум 35 символів).");

            RuleFor(x => x.Description)
                .Must(ContainBackendContext)
                .When(x => x.Type == OrderType.Backend)
                .WithMessage("Для Backend-обряду вкажіть деталі системи (наприклад: БД, SQL, API, сервер, логіка, .NET або архітектура).");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Обрано невідомий тип ритуалу.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Некоректний статус замовлення.");

            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage("Ідентифікатор клієнта не може бути порожнім.");
        }

        private bool NotBeAllCaps(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) return true;
            var letters = title.Where(char.IsLetter).ToList();
            if (letters.Count < 5) return true;

            var cleanLetters = title.Replace("[UI/UX]", "").Where(char.IsLetter).ToList();
            return cleanLetters.Count == 0 || cleanLetters.Any(char.IsLower);
        }

        private bool HaveMeaningfulCharacters(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return false;
            return text.Trim().Distinct().Count() >= 3;
        }

        private bool ContainBackendContext(string description)
        {
            if (string.IsNullOrWhiteSpace(description)) return false;
            var lower = description.ToLowerInvariant();
            string[] keywords = { "бд", "баз", "sql", "api", "сервер", "бекенд", "backend", "логік", "архітектур", ".net", "c#", "таблиц", "даних", "сайт", "систем" };
            return keywords.Any(k => lower.Contains(k));
        }
    }
}