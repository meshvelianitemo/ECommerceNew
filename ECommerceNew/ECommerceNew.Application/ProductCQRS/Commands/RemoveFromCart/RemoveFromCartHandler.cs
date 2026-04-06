
using ECommerceNew.Application.Abstractions;
using MediatR;

namespace ECommerceNew.Application.ProductCQRS.Commands.RemoveFromCart
{
    public class RemoveFromCartHandler : IRequestHandler<RemoveFromCartCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        public RemoveFromCartHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<bool> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            var numberOfAffectedRows = _productRepository.RemoveFromCart(request._Dto.ProductId,
                request._Dto.UserId,
                cancellationToken);

            return numberOfAffectedRows;
        }
    }
}
