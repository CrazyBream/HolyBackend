using System.Linq;
using FluentValidation;
using HolyBackend.Core.Models;

namespace HolyBackend.Core.Validators
{
    public class OrderMessageValidator : AbstractValidator<OrderMessage>
    {
        private static readonly string[] ForbiddenHereticPatterns =
        {
            "<script", "javascript:", "drop table", "delete from", "truncate table", "1=1--"
        };

        public OrderMessageValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Неможливо відправити порожнє повідомлення.")
                .MaximumLength(1000).WithMessage("Повідомлення занадто довге. Спробуйте вкластися в 1000 символів.")
                .Must(NotContainHeresyOrInjections).WithMessage("Інквізиція заблокувала повідомлення: виявлено спробу ін'єкції або заборонений код!")
                .Must(NotBeSpam).WithMessage("Повідомлення схоже на спам однотипними символами.");

            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("Повідомлення має бути прив'язане до заявки.");

            RuleFor(x => x.SenderId)
                .NotEmpty().WithMessage("Відправник невідомий.");
        }

        private bool NotContainHeresyOrInjections(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return true;
            var lower = text.ToLowerInvariant();
            return !ForbiddenHereticPatterns.Any(pattern => lower.Contains(pattern));
        }

        private bool NotBeSpam(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return false;
            var trimmed = text.Trim();
            if (trimmed.Length > 10 && trimmed.Distinct().Count() == 1)
                return false;
            return true;
        }
    }
}