using ECommerceNew.Application.Product.DTOs.ProductDtos;
using MediatR;

namespace ECommerceNew.Application.Product.Queries.GetProductById;

public record GetProductByIdQuery(int Id) : IRequest<ProductDetailDto>;

