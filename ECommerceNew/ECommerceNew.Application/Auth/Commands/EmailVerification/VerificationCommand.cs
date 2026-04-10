
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Auth.DTOs;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Auth.Commands.EmailVerification
{
    public class VerificationCommand : IRequest<Result>
    {
        public EmailVerificationDto _Dto { get; }

        public VerificationCommand(EmailVerificationDto dto)
        {
            _Dto = dto;
        }
    }
}
