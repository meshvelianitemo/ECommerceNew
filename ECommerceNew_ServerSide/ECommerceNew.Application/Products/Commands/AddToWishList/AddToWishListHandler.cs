
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.ProductCQRS.Commands.AddToWishList
{
    public class AddToWishListHandler : IRequestHandler<AddToWishListCommand, Result>
    {
        private readonly IProductRepository _productRepository;
        public AddToWishListHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Result> Handle(AddToWishListCommand request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.AddToWishlist(
                request._Dto.ProductId,
                request._Dto.UserId,
                cancellationToken);
            if (!result.IsSuccess)
            {
                return result;
            }

            return Result.Success();

        }
    }
}
