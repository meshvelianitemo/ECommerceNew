using ECommerceNew.Application.Auth.DTOs;
using ECommerceNew.Application.Results.Errors;
using MediatR;
namespace ECommerceNew.Application.Auth.Commands.PasswordRecovery
{
    public class PasswordResetCommand : IRequest<Result>
    {
        public PasswordResetDto _Dto { get; }

        public PasswordResetCommand(PasswordResetDto dto)
        {
            _Dto = dto;
        }

    }
}
