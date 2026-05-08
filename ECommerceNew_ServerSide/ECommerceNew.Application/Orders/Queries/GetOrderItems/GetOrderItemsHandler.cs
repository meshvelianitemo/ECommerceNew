
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Dashboard.Queries.GetOrderStatus;
using ECommerceNew.Application.Orders.DTOs;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Orders.Queries.GetOrderItems
{
    public class GetOrderItemsHandler : IRequestHandler<GetOrderItemsQuery, Result<List<OrderDetailsDto?>>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrderItemsHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Result<List<OrderDetailsDto?>>> Handle(GetOrderItemsQuery request, CancellationToken cancellationToken)
        {
            var result = await _orderRepository
                .GetOrderByIdAsync(request.OrderId);
            return result;
        }
    }
}
