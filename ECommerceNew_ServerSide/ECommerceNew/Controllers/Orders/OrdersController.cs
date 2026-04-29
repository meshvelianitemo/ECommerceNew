using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Orders.DTOs;
using ECommerceNew.Application.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ECommerceNew.Api.Controllers.Orders
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetOrders([FromQuery] OrderFilter filter, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetFilteredOrdersQuery(filter, cancellationToken), cancellationToken);
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

            return Ok(new { success = result.IsSuccess, value = result.Value });

        }
    }
}
