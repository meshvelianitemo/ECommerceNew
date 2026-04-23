
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Auth.Commands.VerifyRecoveryCode
{
    public class VerifyRecoveryCodeHandler : IRequestHandler<VerifyRecoveryCodeCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        public VerifyRecoveryCodeHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<Result> Handle(VerifyRecoveryCodeCommand request, CancellationToken cancellationToken)
        {
            var result = _userRepository.VerifyVerificationCode
                (request._Dto.Email,
                request._Dto.RecoveryCode,
                cancellationToken);
            return result;
        } 
    }
}
