using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Product.DTOs.ProductDtos;
using ECommerceNew.Domain.Entities.ProductSide;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceNew.Application.Product.Commands.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<CreateProductHandler> _logger;

    public CreateProductHandler(IProductRepository productRepository, ILogger<CreateProductHandler> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        CreateProductDto dto = request._Dto;

        var product = new Domain.Entities.ProductSide.Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Amount = dto.Amount,
            CategoryId = dto.CategoryId,
            UserId = dto.UserId,
            CreationDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow
        };

        var created = await _productRepository.AddAsync(product, cancellationToken);
        _logger.LogInformation("New product created with ProductId {0}", created.ProductId);
        return created.ProductId;
    }
}

