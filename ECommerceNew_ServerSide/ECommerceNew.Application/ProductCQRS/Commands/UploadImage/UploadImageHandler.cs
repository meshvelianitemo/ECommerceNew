
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Responses.Exceptions;
using ECommerceNew.Application.Results.Errors;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceNew.Application.ProductCQRS.Commands.UploadImage
{
    public class UploadImageHandler : IRequestHandler<UploadImageCommand, Result>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<UploadImageHandler> _logger;
        private readonly IStorageRepoistory _storageRepoistory;
        private readonly IUserRepository _userRepository;

        public UploadImageHandler(IProductRepository repo , ILogger<UploadImageHandler> logger, IStorageRepoistory storageRepoistory, IUserRepository userRepository)
        {
            _productRepository  = repo;
            _logger = logger;
            _storageRepoistory = storageRepoistory;
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            
            _logger.LogInformation("Handling UploadImageCommand for Product ID {ProductId}", request._Dto.ProductId);

            var product = await _productRepository.GetByIdAsync(request._Dto.ProductId, cancellationToken);

            if (product == null)
                return Result.Failure(ProductErrors.NotFound);

            var Issuer = await _userRepository.
                GetUserByIdAsync(request._Dto.UserId, cancellationToken);

            if (Issuer == null)
                return Result.Failure(UserErrors.NotFound);

            var imageUrl = await _storageRepoistory.UploadFile(request._Dto.Image, cancellationToken);

            await _productRepository.AddImageUrlAsync(request._Dto.ProductId, imageUrl, request._Dto.UserId, cancellationToken);

            _logger.LogInformation("Image ACTUALLY saved for Product ID {ProductId}, URL: {ImageUrl}", request._Dto.ProductId, imageUrl);

            return Result.Success();


        }
    }
}
