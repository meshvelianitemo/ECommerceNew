using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Orders.Commands.PlaceOrder;
using ECommerceNew.Application.Orders.DTOs;
using ECommerceNew.Application.Orders.Queries.GetOwnOrders;
using ECommerceNew.Domain.enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ServiceStack;

namespace ECommerceNew.Api.Controllers.Orders
{
    [Route("api/[controller]")]
    [Authorize]
    [EnableRateLimiting("token")]

    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<OrdersController> _logger;
        public OrdersController(ISender sender, ILogger<OrdersController> logger, IReviewRepository reviewRepository)
        {
            _logger = logger;
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetOwnOrders([FromQuery] UserOrderFilter filter, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetOwnOrdersQuery(filter, cancellationToken), cancellationToken);
            if (!result.IsSuccess)
            {
                return NotFound(new
                {
                    success = result.IsSuccess,
                    error = new
                    {
                        code = result.Error.Code,
                        message = result.Error.Message,
                        field = result.Error.Field
                    }
                });
            }
            return Ok(new { success = result.IsSuccess, Orders = result.Value });
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received request to create order for user {UserId}", dto.UserId);
            var result = await _sender.Send(new CreateOrderCommand(dto));
            if (!result.IsSuccess)
            {
                return NotFound(new
                {
                    success = result.IsSuccess,
                    error = new
                    {
                        code = result.Error.Code,
                        message = result.Error.Message,
                        field = result.Error.Field
                    }
                });
            }
            return Ok(new { success = result.IsSuccess, message = "Order placed sucessfully!" });
        }
    }
}
