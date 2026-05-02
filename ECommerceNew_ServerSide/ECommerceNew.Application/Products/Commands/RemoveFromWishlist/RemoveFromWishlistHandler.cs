
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.ProductCQRS.Commands.RemoveFromWishlist
{
    public class RemoveFromWishlistHandler : IRequestHandler<RemoveFromWishlistCommand, Result>
    {
        private readonly IProductRepository _productRepository;
        public RemoveFromWishlistHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Result> Handle(RemoveFromWishlistCommand request, CancellationToken cancellationToken)
        {
            var result = await _productRepository
                .RemoveWishListItem(
                request._Dto.ProductId,
                request._Dto.UserId, 
                cancellationToken);

            return result;
        }
    }
}
