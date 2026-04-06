
using FluentValidation;

namespace ECommerceNew.Application.Auth.Commands.PasswordRecoveryCode
{
    public class PasswordRecoveryValidator : AbstractValidator<PasswordRecoveryCommand>
    {
        public PasswordRecoveryValidator()
        {
            RuleFor(x => x._Dto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }
}
