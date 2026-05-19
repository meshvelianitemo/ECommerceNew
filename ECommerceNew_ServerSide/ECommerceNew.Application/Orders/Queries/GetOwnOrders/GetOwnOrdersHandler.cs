
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Orders.DTOs;
using ECommerceNew.Application.Results.Errors;
using MediatR;
using System.Runtime.CompilerServices;

namespace ECommerceNew.Application.Orders.Queries.GetOwnOrders
{
    public class GetOwnOrdersHandler : IRequestHandler<GetOwnOrdersQuery, Result<List<OrderDto>>>
    {

        private readonly IOrderRepository _orderRepository;
        public GetOwnOrdersHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Result<List<OrderDto>>> Handle(GetOwnOrdersQuery request, CancellationToken cancellationToken)
        {
            var result = await _orderRepository
                .GetUserOrders(request.filter);

            return Result<List<OrderDto>>.Success(result);
        }
    }
}
