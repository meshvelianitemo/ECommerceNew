
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Responses.Exceptions;
using ECommerceNew.Application.Results.Errors;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceNew.Application.ProductCQRS.Commands.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Result>
    {
        private readonly ILogger<UpdateProductHandler> _logger;
        private readonly IProductRepository _productRepository;
        public UpdateProductHandler(ILogger<UpdateProductHandler> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var dto = request._Dto;

            var product = await _productRepository.GetByIdAsync(dto.ProductId, cancellationToken);

            if (product == null)
            {
                return Result.Failure(ProductErrors.NotFound);
            }

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Amount = dto.Amount;
            product.ModifiedDate = DateTime.UtcNow;

            var result = await _productRepository.UpdateAsync(product, cancellationToken);
            _logger.LogInformation("Product with ProductId {0} is updated successfully.", dto.ProductId);

            return result;
        }
    }
}
