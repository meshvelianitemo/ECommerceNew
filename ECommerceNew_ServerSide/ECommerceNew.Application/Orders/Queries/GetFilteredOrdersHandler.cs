
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Orders.DTOs;
using ECommerceNew.Application.Results.Errors;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceNew.Application.Orders.Queries
{
    public class GetFilteredOrdersHandler : IRequestHandler<GetFilteredOrdersQuery, Result<List<OrderDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<GetFilteredOrdersHandler> _logger;
        private readonly IUserRepository _userRepository;
        public GetFilteredOrdersHandler(IOrderRepository orderRepository,
            ILogger<GetFilteredOrdersHandler> logger,
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
            _orderRepository = orderRepository;
        }
        public async Task<Result<List<OrderDto>>> Handle(GetFilteredOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository
                .GetOrdersAsync(request.filter);
            
            return Result<List<OrderDto>>.Success(orders.Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                CustomerName = o.CustomerName,
                TotalAmount = o.TotalAmount,
                Status = o.Status,
                OrderDate = o.OrderDate,
                PhoneNumber = o.PhoneNumber,
                Address = o.Address
            }).ToList());

        }
    }
}
