
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.ProductCQRS.Commands.RemoveFromCart
{
    public class RemoveFromCartHandler : IRequestHandler<RemoveFromCartCommand, Result>
    {
        private readonly IProductRepository _productRepository;
        public RemoveFromCartHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<Result> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            var numberOfAffectedRows = _productRepository.RemoveFromCart(request._Dto.ProductId,
                request._Dto.UserId,
                cancellationToken);

            return numberOfAffectedRows;
        }
    }
}
