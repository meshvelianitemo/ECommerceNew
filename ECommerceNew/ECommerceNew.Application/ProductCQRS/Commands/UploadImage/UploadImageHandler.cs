
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceNew.Application.ProductCQRS.Commands.UploadImage
{
    public class UploadImageHandler : IRequestHandler<UploadImageCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<UploadImageHandler> _logger;
        private readonly IStorageRepoistory _storageRepoistory;

        public UploadImageHandler(IProductRepository repo , ILogger<UploadImageHandler> logger, IStorageRepoistory storageRepoistory)
        {
            _productRepository  = repo;
            _logger = logger;
            _storageRepoistory = storageRepoistory;
        }

        public async Task<bool> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            
            _logger.LogInformation("Handling UploadImageCommand for Product ID {ProductId}", request._Dto.ProductId);

            var product = await _productRepository.GetByIdAsync(request._Dto.ProductId, cancellationToken);

            if (product == null)
                throw new ProductNotFoundException($"Product with ID {request._Dto.ProductId} not found.");

            var imageUrl = await _storageRepoistory.UploadFile(request._Dto.Image, cancellationToken);

            await _productRepository.AddImageUrlAsync(request._Dto.ProductId, imageUrl, request._Dto.UserId, cancellationToken);

            _logger.LogInformation("Image ACTUALLY saved for Product ID {ProductId}, URL: {ImageUrl}", request._Dto.ProductId, imageUrl);

            return true;


        }
    }
}
