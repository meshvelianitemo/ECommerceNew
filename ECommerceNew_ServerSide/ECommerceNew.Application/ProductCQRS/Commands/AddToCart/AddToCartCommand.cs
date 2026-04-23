
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;
using MediatR;
using System.Drawing;

namespace ECommerceNew.Application.ProductCQRS.Commands.AddToCart
{
    public class AddToCartCommand : IRequest<Result>
    {
        public AddToCartDto _Dto { get; set; }

        public AddToCartCommand(AddToCartDto dto)
        {
            _Dto = dto;
        }

    }
}
