
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Exceptions;
using ECommerceNew.Application.ProductCQRS.Commands.DeleteProduct;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace ECommerceNew.Application.ProductCQRS.Commands.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly ILogger<UpdateProductHandler> _logger;
        private readonly IProductRepository _productRepository;
        public UpdateProductHandler(ILogger<UpdateProductHandler> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var dto = request._Dto;

            var product = await _productRepository.GetByIdAsync(dto.ProductId, cancellationToken);

            if (product == null)
            {
                throw new ProductNotFoundException($"Product with ID {dto.ProductId} not found.");
            }

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Amount = dto.Amount;
            product.ModifiedDate = DateTime.UtcNow;

            await _productRepository.UpdateAsync(product, cancellationToken);
            _logger.LogInformation("Product with ProductId {0} is updated successfully.", dto.ProductId);

            return true;
        }
    }
}
