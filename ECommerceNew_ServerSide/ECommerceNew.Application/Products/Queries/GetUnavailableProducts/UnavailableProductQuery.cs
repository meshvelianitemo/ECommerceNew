
using ECommerceNew.Application.Product.DTOs.ProductDtos;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Products.Queries.GetUnavailableProducts
{
    public class UnavailableProductQuery : IRequest<Result<PagedResult<ProductDetailDto>>>
    {
        public ProductQueryParameters QueryParams { get; set; }
        public UnavailableProductQuery(ProductQueryParameters queryParams)
        {
            QueryParams = queryParams;
        }
    }
}
