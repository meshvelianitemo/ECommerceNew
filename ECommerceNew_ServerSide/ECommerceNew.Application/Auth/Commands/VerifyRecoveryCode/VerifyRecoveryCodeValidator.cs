
using FluentValidation;

namespace ECommerceNew.Application.Auth.Commands.VerifyRecoveryCode
{
    public class VerifyRecoveryCodeValidator : AbstractValidator<VerifyRecoveryCodeCommand>
    {
        public VerifyRecoveryCodeValidator()
        {
            RuleFor(x => x._Dto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }
}
