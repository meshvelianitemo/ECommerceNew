
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Auth.DTOs;
using MediatR;

namespace ECommerceNew.Application.Auth.Commands.EmailVerification
{
    public class VerificationCommand : IRequest<bool>
    {
        public EmailVerificationDto _Dto { get; }

        public VerificationCommand(EmailVerificationDto dto)
        {
            _Dto = dto;
        }
    }
}
