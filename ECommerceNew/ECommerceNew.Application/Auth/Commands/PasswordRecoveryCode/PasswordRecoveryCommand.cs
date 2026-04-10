
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Auth.DTOs;
using ECommerceNew.Application.Results.Errors;
using MediatR;
using System.Drawing;

namespace ECommerceNew.Application.Auth.Commands.PasswordRecoveryCode
{
    public class PasswordRecoveryCommand : IRequest<Result>
    {
        public readonly PasswordRecoveryEmailDto _Dto;

        public PasswordRecoveryCommand(PasswordRecoveryEmailDto dto)
        {
            _Dto = dto;
        }
    }
}
