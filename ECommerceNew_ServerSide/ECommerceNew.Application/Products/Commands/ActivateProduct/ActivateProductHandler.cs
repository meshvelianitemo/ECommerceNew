
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.ProductCQRS.Commands.ActivateProduct
{
    public class ActivateProductHandler : IRequestHandler<ActivateProductCommand, Result>
    {
        private readonly IProductRepository _productRepository;
        public ActivateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Result> Handle(ActivateProductCommand request, CancellationToken cancellationToken)
        { 
            var result = await _productRepository
                .ActivateProduct(request.ProductId, cancellationToken);

            return result;
        }
    }
}
