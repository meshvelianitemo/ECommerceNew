
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;
using MediatR;
using System.Drawing;

namespace ECommerceNew.Application.ProductCQRS.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Result>
    {
        public UpdateProductDto _Dto { get; }

        public UpdateProductCommand(UpdateProductDto dto)
        {
            _Dto = dto;
        }
    }
}
