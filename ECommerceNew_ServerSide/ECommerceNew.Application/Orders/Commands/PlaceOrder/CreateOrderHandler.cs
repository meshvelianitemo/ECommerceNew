
using Amazon.Runtime;
using Amazon.Runtime.Internal.Util;
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Results.Errors;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;

namespace ECommerceNew.Application.Orders.Commands.PlaceOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<CreateOrderHandler> _logger;
        private readonly ICartRepository _cartRepository;
        public CreateOrderHandler(IOrderRepository orderRepository, 
            ILogger<CreateOrderHandler> logger,
            ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            _logger = logger;
            _orderRepository = orderRepository;
        }
        public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            // COMBINING BOTH STEPS INTO ONE TO ENSURE ATOMICITY, IF ANY STEP FAILS,
            // THE ENTIRE OPERATION WILL BE ROLLED BACK
            var orderResult = await _orderRepository
                .CreateOrderWithItemsAsync(request._Dto, cancellationToken);
            if (!orderResult.IsSuccess)
            {
                return orderResult;
            }
            _logger.LogInformation("placed order, by user: #{0}", request._Dto.UserId);
            // SINCE THE WHOLE CART GOT ORDERED, WE SHOULD CLEAR THE CART FOR THE USER 
            var cartResult = await _cartRepository
                .ClearCart(request._Dto.UserId, cancellationToken);

            if (!cartResult.IsSuccess)
            {
                _logger.LogError("Failed to clear cart for user {UserId} after placing order. Error: {ErrorMessage}", request._Dto.UserId, cartResult.Error.Message);
                // Depending on the business logic, you might want to return a failure here or just log the error and continue
                // For this example, we will log the error but still return success for the order creation
            }
            return orderResult;
        }
    }
}

