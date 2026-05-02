using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Responses.Exceptions;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Cart.Commands.AddToCart
{
    public class AddToCartHandler : IRequestHandler<AddToCartCommand, Result>
    {
        private readonly ICartRepository _cartReposistory;

        public AddToCartHandler(ICartRepository cartReposistory)
        {
            _cartReposistory = cartReposistory;
        }

        public async Task<Result> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var result = await _cartReposistory.AddToCart(request._Dto.ProductId,
                request._Dto.UserId,
                request._Dto.Quantity
                );

            if (!result.IsSuccess)
            {
                return result;
            }
            return Result.Success();
        }
    }
}
