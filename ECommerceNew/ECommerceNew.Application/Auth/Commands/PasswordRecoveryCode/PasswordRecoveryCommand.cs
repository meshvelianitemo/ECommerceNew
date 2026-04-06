
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Auth.DTOs;
using MediatR;
using System.Drawing;

namespace ECommerceNew.Application.Auth.Commands.PasswordRecoveryCode
{
    public class PasswordRecoveryCommand : IRequest<bool>
    {
        public readonly PasswordRecoveryEmailDto _Dto;

        public PasswordRecoveryCommand(PasswordRecoveryEmailDto dto)
        {
            _Dto = dto;
        }
    }
}
