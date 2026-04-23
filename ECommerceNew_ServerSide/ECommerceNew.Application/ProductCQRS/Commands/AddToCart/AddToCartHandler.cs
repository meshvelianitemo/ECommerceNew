
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Responses.Exceptions;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.ProductCQRS.Commands.AddToCart
{
    public class AddToCartHandler : IRequestHandler<AddToCartCommand, Result>
    {
        private readonly IProductRepository _productReposistory;

        public AddToCartHandler(IProductRepository productReposistory)
        {
            _productReposistory = productReposistory;
        }

        public async Task<Result> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var result = await _productReposistory.AddToCart(request._Dto.ProductId,
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
