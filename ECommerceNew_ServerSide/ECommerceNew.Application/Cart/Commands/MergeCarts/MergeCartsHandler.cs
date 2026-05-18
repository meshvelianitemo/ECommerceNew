
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Cart.Commands.MergeCarts
{
    public class MergeCartsHandler : IRequestHandler<MergeCartsCommand, Result>
    {
       private readonly ICartRepository _cartRepository;
        public MergeCartsHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<Result> Handle(MergeCartsCommand request, CancellationToken cancellationToken)
        {
            var result = await _cartRepository
                .MergeCarts(request.CartItems ,cancellationToken);
            return result;
        }

    }
}
