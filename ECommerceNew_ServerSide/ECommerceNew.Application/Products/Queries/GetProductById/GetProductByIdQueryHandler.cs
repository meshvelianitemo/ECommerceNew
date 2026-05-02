using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Product.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Product.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDetailDto?>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<ProductDetailDto?>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
        {
            return Result<ProductDetailDto>.Failure(ProductErrors.NotFound);
        }
        var productDetailDto = new ProductDetailDto
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Amount = product.Amount,
            CategoryId = product.CategoryId,
            CategoryName = product.ProductCategory?.CategoryName ?? string.Empty,
            //imageUrl = (ICollection<string>)product.ProductImages,
            CreationDate = product.CreationDate, 
            ModifiedDate = product.ModifiedDate
            
        };

        return Result<ProductDetailDto>.Success(productDetailDto);
    }
}

