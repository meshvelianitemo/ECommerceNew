
using Amazon.Runtime.Internal;
using Amazon.S3.Model;
using ECommerceNew.Application.Auth.DTOs;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Auth.Commands.PasswordChange
{
    public class PasswordChangeCommand :IRequest<Result>
    {
        public PasswordChangeDto Dto { get; }
        public PasswordChangeCommand(PasswordChangeDto dto)
        {
            Dto = dto;
        }
    }
}
