using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;
using MediatR;
using Microsoft.Extensions.Logging;
namespace ECommerceNew.Application.ProductCQRS.Commands.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Result>
    {
        private readonly ILogger<DeleteProductHandler> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        public DeleteProductHandler(IUserRepository userRepository , ILogger<DeleteProductHandler> logger, IProductRepository productRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
            _productRepository = productRepository;
        }
        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            DeleteProductDto dto = request._Dto;    
            var user = await _userRepository
                .GetUserByIdAsync(dto.UserId, cancellationToken);
            var product = await _productRepository
                .GetByIdAsync(dto.ProductId, cancellationToken);
            if (user == null)
            {
                _logger.LogWarning("User with UserId {0} is not found", dto.UserId);
                return Result.Failure(UserErrors.NotFound);
            }
            if (product == null)
            {
                _logger.LogWarning("Product with ProductId {0} is not found", dto.ProductId);
                return Result.Failure(ProductErrors.NotFound);
            }
            await _productRepository.DeleteAsync(product, cancellationToken);
            _logger.LogInformation("Product with ProductId {0} is deleted", product.ProductId);

            return Result.Success();
        }
    }
}
