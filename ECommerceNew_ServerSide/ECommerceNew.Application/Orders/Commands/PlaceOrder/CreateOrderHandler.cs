
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
            //var orderResult = await _orderRepository
            //    .CreateOrderAsync(request._Dto, cancellationToken);
            //if (!orderResult.IsSuccess)
            //{
            //    return orderResult;
            //}

            //var orderItemsResult = await _orderRepository
            //    .AddOrderItems(request._Dto.Items,orderResult.Value, cancellationToken);
            //return orderItemsResult;

            // COMBINING BOTH STEPS INTO ONE TO ENSURE ATOMICITY, IF ANY STEP FAILS, THE ENTIRE OPERATION WILL BE ROLLED BACK
            var result = await _orderRepository
                .CreateOrderWithItemsAsync(request._Dto, cancellationToken);
            return result;
        }
    }
}
