using ECommerceNew.Application.Product.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Product.Queries.GetProductById;

public record GetProductByIdQuery(int Id) : IRequest<Result<ProductDetailDto>>;

