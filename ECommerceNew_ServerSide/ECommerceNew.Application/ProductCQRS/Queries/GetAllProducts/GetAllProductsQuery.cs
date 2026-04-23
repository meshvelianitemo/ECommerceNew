using ECommerceNew.Application.Product.DTOs.ProductDtos;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;
using MediatR;
namespace ECommerceNew.Application.ProductCQRS.Queries.GetAllProducts
{
    public record GetAllProductsQuery(ProductQueryParameters queryParams) : IRequest<Result<PagedResult<ProductDetailDto>>>;
}
