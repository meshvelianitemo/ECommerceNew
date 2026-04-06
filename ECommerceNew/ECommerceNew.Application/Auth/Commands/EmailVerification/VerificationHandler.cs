
using Amazon.Runtime;
using ECommerceNew.Application.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceNew.Application.Auth.Commands.EmailVerification
{
    public class VerificationHandler : IRequestHandler<VerificationCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public VerificationHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<bool> Handle(VerificationCommand request, CancellationToken cancellationToken)
        {
            var result =  _userRepository.VerifyVerificationCode(request._Dto.Email, request._Dto.VerificationCode, cancellationToken);

            return result; 
        }
    }
}
