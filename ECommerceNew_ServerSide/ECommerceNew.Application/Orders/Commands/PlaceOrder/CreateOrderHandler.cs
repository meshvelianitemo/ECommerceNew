
using Amazon.Runtime;
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Orders.Commands.PlaceOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;
        public CreateOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // THIS ORDER RESULT RETURNS ORDERID IF SUCCESSFUL,
            // THAT WILL BE USED TO ADD ORDER ITEMS IN THE NEXT STEP
            var orderResult = await _orderRepository
                .CreateOrderAsync(request._Dto, cancellationToken);
            if (!orderResult.IsSuccess)
            {
                return orderResult;
            }

            var orderItemsResult = await _orderRepository
                .AddOrderItems(request._Dto.Items,orderResult.Value, cancellationToken);
            return orderItemsResult;
        }
    }
}
