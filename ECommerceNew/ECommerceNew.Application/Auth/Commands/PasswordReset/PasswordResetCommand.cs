using ECommerceNew.Application.Auth.DTOs;
using MediatR;
namespace ECommerceNew.Application.Auth.Commands.PasswordRecovery
{
    public class PasswordResetCommand : IRequest<bool>
    {
        public PasswordResetDto _Dto { get; }

        public PasswordResetCommand(PasswordResetDto dto)
        {
            _Dto = dto;
        }

    }
}
