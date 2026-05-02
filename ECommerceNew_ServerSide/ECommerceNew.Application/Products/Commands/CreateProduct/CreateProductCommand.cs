
using ECommerceNew.Application.Product.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Product.Commands.CreateProduct;

public class CreateProductCommand : IRequest<Result<int>>
{
    public CreateProductDto _Dto { get; }

    public CreateProductCommand(CreateProductDto dto)
    {
        _Dto = dto;
    }
}

