using FluentValidation;
using HolyBackend.Core.Models;

namespace HolyBackend.Core.Validators
{
    public class OrderMessageValidator : AbstractValidator<OrderMessage>
    {
        public OrderMessageValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Неможливо відправити порожнє повідомлення.")
                .MaximumLength(1000).WithMessage("Повідомлення занадто довге. Спробуйте вкластися в 1000 символів.");

            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("Повідомлення має бути прив'язане до заявки.");

            RuleFor(x => x.SenderId)
                .NotEmpty().WithMessage("Відправник невідомий.");
        }
    }
}