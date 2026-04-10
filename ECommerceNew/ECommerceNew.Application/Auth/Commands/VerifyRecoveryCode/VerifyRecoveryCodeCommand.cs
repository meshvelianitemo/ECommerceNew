
using ECommerceNew.Application.Auth.DTOs;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Auth.Commands.VerifyRecoveryCode
{
    public class VerifyRecoveryCodeCommand : IRequest<Result>
    {
        public RecoveryCodeVerificationDto _Dto { get; set; }
        public VerifyRecoveryCodeCommand(RecoveryCodeVerificationDto dto)
        {
            _Dto = dto;
        }
    
    }
}
