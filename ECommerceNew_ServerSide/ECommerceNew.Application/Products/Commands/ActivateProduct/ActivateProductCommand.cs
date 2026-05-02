
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.ProductCQRS.Commands.ActivateProduct
{
    public class ActivateProductCommand : IRequest<Result>
    {
        public int ProductId { get; }
        public ActivateProductCommand(int productId)
        {
            ProductId = productId;
        }

    }
}
