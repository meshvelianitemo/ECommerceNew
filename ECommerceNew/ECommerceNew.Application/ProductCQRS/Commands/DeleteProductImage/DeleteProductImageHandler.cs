using ECommerceNew.Application.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceNew.Application.ProductCQRS.Commands.DeleteProductImage
{
    public class DeleteProductImageHandler : IRequestHandler<DeleteProductImageCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IStorageRepoistory _storageRepository;
        private readonly ILogger<DeleteProductImageHandler> _logger;

        public DeleteProductImageHandler(IProductRepository productRepository,
            IStorageRepoistory storageRepository , ILogger<DeleteProductImageHandler> logger)
        {
            _productRepository = productRepository;
            _storageRepository = storageRepository;
            _logger = logger;
        }
        public async Task<bool> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
        {

            var dbRemovalResult =await _productRepository.RemoveImageUrlAsync
                (request._Dto.ProductId, request._Dto.ImageKey, cancellationToken);

            if (!dbRemovalResult)
            {
                _logger.LogWarning("Failed to remove image URL from database for ProductId: {ProductId}", request._Dto.ProductId);
                return false;
            }

            var bucketRemovalResult = await _storageRepository.DeleteImageAsync
                (request._Dto.ImageKey, cancellationToken);

            if (!bucketRemovalResult)
            {
                _logger.LogWarning("Failed to remove image from storage key: {}", request._Dto.ImageKey);
                return false;
            }

            return true;
        }
    }
}
