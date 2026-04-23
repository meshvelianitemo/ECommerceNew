
using FluentValidation;

namespace ECommerceNew.Application.Auth.Commands.EmailVerification
{
    public class VerificationValidator : AbstractValidator<VerificationCommand>
    {
            public VerificationValidator()
            {
                RuleFor(x => x._Dto.VerificationCode).NotEmpty().WithMessage("Verification code is required.");
        }
    }
}
