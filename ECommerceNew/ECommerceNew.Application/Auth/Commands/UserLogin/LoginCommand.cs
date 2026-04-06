
using ECommerceNew.Application.Auth.DTOs;
using ECommerceNew.Domain.Entities.UserSide;
using MediatR;

namespace ECommerceNew.Application.Auth.Commands.Login
{
    public class LoginCommand : IRequest<User>
    {
        public LoginRequest _Dto { get; }

        public LoginCommand(LoginRequest dto)
        {
            _Dto = dto; 
        }
    }
}
