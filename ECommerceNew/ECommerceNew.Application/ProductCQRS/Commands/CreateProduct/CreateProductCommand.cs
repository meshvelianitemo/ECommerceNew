
using ECommerceNew.Application.Product.DTOs.ProductDtos;
using MediatR;

namespace ECommerceNew.Application.Product.Commands.CreateProduct;

public class CreateProductCommand : IRequest<int>
{
    public CreateProductDto _Dto { get; }

    public CreateProductCommand(CreateProductDto dto)
    {
        _Dto = dto;
    }
}

