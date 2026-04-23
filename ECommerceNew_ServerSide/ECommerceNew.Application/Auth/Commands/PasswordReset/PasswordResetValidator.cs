
using ECommerceNew.Application.Auth.Commands.PasswordRecovery;
using FluentValidation;

namespace ECommerceNew.Application.Auth.Commands
{
    public class PasswordResetValidator : AbstractValidator<PasswordResetCommand>
    {
        public PasswordResetValidator()
        {
            RuleFor(x => x._Dto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }
}
