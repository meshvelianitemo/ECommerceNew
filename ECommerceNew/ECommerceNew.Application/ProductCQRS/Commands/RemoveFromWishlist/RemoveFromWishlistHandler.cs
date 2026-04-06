
using ECommerceNew.Application.Abstractions;
using MediatR;

namespace ECommerceNew.Application.ProductCQRS.Commands.RemoveFromWishlist
{
    public class RemoveFromWishlistHandler : IRequestHandler<RemoveFromWishlistCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        public RemoveFromWishlistHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Task<bool> Handle(RemoveFromWishlistCommand request, CancellationToken cancellationToken)
        {
            var result = _productRepository
                .RemoveWishListItem(
                request._Dto.ProductId,
                request._Dto.UserId, 
                cancellationToken);
            return result;
        }
    }
}
