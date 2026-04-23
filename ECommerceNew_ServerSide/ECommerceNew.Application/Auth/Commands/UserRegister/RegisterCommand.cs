
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Auth.DTOs;
using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Domain.Entities.UserSide;
using MediatR;

namespace ECommerceNew.Application.Auth.Commands.UserRegister
{ 
    public class RegisterCommand : IRequest<Result<User>>
    {
        public RegisterRequest _Dto { get; }
        public RegisterCommand(RegisterRequest dto)
        {
            _Dto = dto;
        }
    }
}
