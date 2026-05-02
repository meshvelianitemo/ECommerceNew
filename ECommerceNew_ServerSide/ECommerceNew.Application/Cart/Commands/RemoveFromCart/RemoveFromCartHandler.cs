using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Cart.Commands.RemoveFromCart
{
    public class RemoveFromCartHandler : IRequestHandler<RemoveFromCartCommand, Result>
    {
        private readonly ICartRepository _cartRepository;
        public RemoveFromCartHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public Task<Result> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            var numberOfAffectedRows = _cartRepository.RemoveFromCart(request._Dto.ProductId,
                request._Dto.UserId,
                cancellationToken);

            return numberOfAffectedRows;
        }
    }
}
