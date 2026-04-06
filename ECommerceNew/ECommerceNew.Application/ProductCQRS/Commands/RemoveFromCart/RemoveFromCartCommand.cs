using MediatR;
using Amazon.Runtime.Internal;
using System.Drawing;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;

namespace ECommerceNew.Application.ProductCQRS.Commands.RemoveFromCart
{
    public class RemoveFromCartCommand : IRequest<bool>
    {
        public RemoveFromCartDto _Dto;
        public RemoveFromCartCommand(RemoveFromCartDto dto)
        {
            _Dto = dto;
        }
    }
}
