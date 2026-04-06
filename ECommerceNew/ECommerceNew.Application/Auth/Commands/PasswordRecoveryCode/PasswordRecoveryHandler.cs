
using ECommerceNew.Application.Abstractions;
using MediatR;

namespace ECommerceNew.Application.Auth.Commands.PasswordRecoveryCode
{
    public class PasswordRecoveryHandler : IRequestHandler<PasswordRecoveryCommand, bool>
    {
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository; 

        public PasswordRecoveryHandler(IEmailService emailService, IUserRepository userRepository)
        {
            _emailService = emailService;
            _userRepository = userRepository;
        }
        public async Task<bool> Handle(PasswordRecoveryCommand request, CancellationToken cancellationToken)
        {
            var verificationCode = await _emailService.SendPasswordRecoveryEmailAsync(request._Dto.Email);

            await _userRepository.AddEmailVerificationRecord(verificationCode, request._Dto.Email);

            return true;
        }
    }
}
