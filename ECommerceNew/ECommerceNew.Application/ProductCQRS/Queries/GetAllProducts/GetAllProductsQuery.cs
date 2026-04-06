using ECommerceNew.Application.Product.DTOs.ProductDtos;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using MediatR;
namespace ECommerceNew.Application.ProductCQRS.Queries.GetAllProducts
{
    public record GetAllProductsQuery(ProductQueryParameters queryParams) : IRequest<PagedResult<ProductDetailDto>>;
}
