
using ECommerceNew.Application.Abstractions;
using MediatR;

namespace ECommerceNew.Application.Auth.Commands.VerifyRecoveryCode
{
    public class VerifyRecoveryCodeHandler : IRequestHandler<VerifyRecoveryCodeCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        public VerifyRecoveryCodeHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<bool> Handle(VerifyRecoveryCodeCommand request, CancellationToken cancellationToken)
        {
            var result = _userRepository.VerifyVerificationCode(request._Dto.Email, request._Dto.RecoveryCode, cancellationToken);
            return result;
        } 
    }
}
