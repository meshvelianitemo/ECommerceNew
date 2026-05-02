
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Product.DTOs.ProductDtos;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Products.Queries.GetUnavailableProducts
{
    public class UnavailableProductHandler : IRequestHandler<UnavailableProductQuery, Result<PagedResult<ProductDetailDto>>>
    {
        private readonly IProductRepository _productRepository;
        public UnavailableProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Result<PagedResult<ProductDetailDto>>> Handle(UnavailableProductQuery request, CancellationToken cancellationToken)
        {
            var result = await _productRepository
                .ListAsync(
                request.QueryParams,
                isActive: false,
                cancellationToken)
                ;
            return Result<PagedResult<ProductDetailDto>>.Success(result);
        }
    }
}

