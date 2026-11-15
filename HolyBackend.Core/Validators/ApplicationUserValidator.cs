using FluentValidation;
using HolyBackend.Core.Models;

namespace HolyBackend.Core.Validators
{
    public class ApplicationUserValidator : AbstractValidator<ApplicationUser>
    {
        public ApplicationUserValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Ім'я парафіянина не може бути порожнім.")
                .Length(3, 50).WithMessage("Ім'я має містити від 3 до 50 символів.")
                .Matches(@"^[a-zA-Zа-яА-ЯіІїЇєЄґҐ0-9\s\-_'.]+$")
                .WithMessage("Ім'я містить недопустимі символи.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Електронна пошта є обов'язковою.")
                .EmailAddress().WithMessage("Некоректний формат електронної пошти.");

            RuleFor(x => x.PasswordHash)
                .NotEmpty().WithMessage("Хеш пароля не може бути порожнім.");
        }
    }
}