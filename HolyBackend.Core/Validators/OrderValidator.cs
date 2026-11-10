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
                .Length(5, 100).WithMessage("Назва має містити від 5 до 100 символів.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Опис проблеми є обов'язковим.")
                .MinimumLength(20).WithMessage("Опис має містити хоча б 20 символів для точного розуміння гріха.");

            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage("Ідентифікатор клієнта не може бути порожнім.");
        }
    }
}
