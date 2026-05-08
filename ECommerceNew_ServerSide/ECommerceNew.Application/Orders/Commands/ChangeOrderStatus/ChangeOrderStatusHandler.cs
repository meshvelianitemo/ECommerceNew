
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Orders.Commands.ChangeOrderStatus
{
    public class ChangeOrderStatusHandler : IRequestHandler<ChangeOrderStatusCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;
        public ChangeOrderStatusHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Result> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var result = await _orderRepository
                .UpdateOrderStatus(request.UpdateDto, cancellationToken);
            return result;
        }
    }
}
