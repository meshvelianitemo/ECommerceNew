
using ECommerceNew.Application.Auth.DTOs;
using MediatR;

namespace ECommerceNew.Application.Auth.Commands.VerifyRecoveryCode
{
    public class VerifyRecoveryCodeCommand : IRequest<bool>
    {
        public RecoveryCodeVerificationDto _Dto { get; set; }
        public VerifyRecoveryCodeCommand(RecoveryCodeVerificationDto dto)
        {
            _Dto = dto;
        }
    
    }
}
