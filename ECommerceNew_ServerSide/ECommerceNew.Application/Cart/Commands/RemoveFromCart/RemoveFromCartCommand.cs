using MediatR;
using Amazon.Runtime.Internal;
using System.Drawing;
using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Application.Cart.Dtos;

namespace ECommerceNew.Application.Cart.Commands.RemoveFromCart
{
    public class RemoveFromCartCommand : IRequest<Result>
    {
        public RemoveFromCartDto _Dto;
        public RemoveFromCartCommand(RemoveFromCartDto dto)
        {
            _Dto = dto;
        }
    }
}
