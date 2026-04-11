using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Product.DTOs.ProductDtos;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceNew.Application.ProductCQRS.Queries.GetAllProducts
{
    
    public class GetAllProductsQueryHandler 
        : IRequestHandler<GetAllProductsQuery, Result<PagedResult<ProductDetailDto>>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<PagedResult<ProductDetailDto>>> Handle(
            GetAllProductsQuery request,
            CancellationToken cancellationToken)
        {
            var products = await _productRepository.ListAsync(
                request.queryParams,
                cancellationToken);
            var result = Result<PagedResult<ProductDetailDto>>.Success(products);
            return result;
        }
    }
}


