
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using MediatR;

namespace ECommerceNew.Application.ProductCQRS.Commands.AddToWishList
{
    public class AddToWishListHandler : IRequestHandler<AddToWishListCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        public AddToWishListHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> Handle(AddToWishListCommand request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.AddToWishlist(
                request._Dto.ProductId,
                request._Dto.UserId,
                cancellationToken);

            return result;

        }
    }
}
