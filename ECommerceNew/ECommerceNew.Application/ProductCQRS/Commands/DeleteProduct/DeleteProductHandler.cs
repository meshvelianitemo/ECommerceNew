using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using MediatR;
using Microsoft.Extensions.Logging;
namespace ECommerceNew.Application.ProductCQRS.Commands.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly ILogger<DeleteProductHandler> _logger;
        private readonly IProductRepository _productRepository;
        public DeleteProductHandler(ILogger<DeleteProductHandler> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            DeleteProductDto dto = request._Dto;    

            var Existing = await _productRepository.GetByIdAsync(dto.ProductId, cancellationToken);
            if (Existing != null)
            {
                await _productRepository.DeleteAsync(Existing, cancellationToken);
                _logger.LogInformation("Product with ProductId {0} is deleted", Existing.ProductId);

                return true;
            }
            _logger.LogWarning("Product with ProductId {0} is not found", dto.ProductId);

            return false;
        }
    }
}
