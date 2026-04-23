using MediatR;
using Amazon.Runtime.Internal;
using System.Drawing;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;

namespace ECommerceNew.Application.ProductCQRS.Commands.RemoveFromCart
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
