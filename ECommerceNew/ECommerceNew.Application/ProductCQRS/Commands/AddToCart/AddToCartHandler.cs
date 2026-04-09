
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Responses.Exceptions;
using MediatR;

namespace ECommerceNew.Application.ProductCQRS.Commands.AddToCart
{
    public class AddToCartHandler : IRequestHandler<AddToCartCommand, bool>
    {
        private readonly IProductRepository _productReposistory;

        public AddToCartHandler(IProductRepository productReposistory)
        {
            _productReposistory = productReposistory;
        }

        public async Task<bool> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var result = await _productReposistory.AddToCart(request._Dto.ProductId,
                request._Dto.UserId,
                request._Dto.Quantity
                );

            if (!result)
            {
                throw new DbUpdateException("Adding product to cart failed!");
            }
            return true;
        }
    }
}
