
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Auth.Commands.PasswordRecoveryCode
{
    public class PasswordRecoveryHandler : IRequestHandler<PasswordRecoveryCommand, Result>
    {
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository; 

        public PasswordRecoveryHandler(IEmailService emailService, IUserRepository userRepository)
        {
            _emailService = emailService;
            _userRepository = userRepository;
        }
        public async Task<Result> Handle(PasswordRecoveryCommand request, CancellationToken cancellationToken)
        {
            var verificationCode = await _emailService
                .SendPasswordRecoveryEmailAsync(request._Dto.Email);

            var resultOfAddingRecord = await _userRepository
                .AddEmailVerificationRecord(verificationCode, request._Dto.Email);
            if (resultOfAddingRecord == null)
            {
                return Result.Failure(UserErrors.ApplicationError);
            }
            return Result.Success();
        }
    }
}
