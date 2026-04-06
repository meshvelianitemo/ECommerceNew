using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Product.DTOs.ProductDtos;
using MediatR;

namespace ECommerceNew.Application.Product.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDetailDto?>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDetailDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
        {
            return null;
        }

        return new ProductDetailDto
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Amount = product.Amount,
            CategoryId = product.CategoryId,
            CategoryName = product.ProductCategory?.CategoryName ?? string.Empty
        };
    }
}

