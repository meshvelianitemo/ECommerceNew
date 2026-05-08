
using ECommerceNew.Application.Orders.Commands.ChangeOrderStatus;
using ECommerceNew.Application.Orders.DTOs;
using ECommerceNew.Application.Orders.Queries.FilteredOrdersQuery;
using ECommerceNew.Application.Orders.Queries.GetOrderItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ECommerceNew.Api.Controllers.Orders
{
    [Route("api/[controller]")]
    //[Authorize(Roles= "Admin")]
    [EnableRateLimiting("token")]
    [ApiController]
    public class AdminOrderController : ControllerBase
    {
        private readonly ISender _sender;
        public AdminOrderController(ISender sender)
        {
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

        [HttpGet("{orderId}/items")]
        public async Task<IActionResult> GetOrderDetails([FromQuery] int orderId)
        {
            var result = await _sender.Send(new GetOrderItemsQuery(orderId));

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

        [HttpPut]
        public async Task<IActionResult> ChangeOrderStatus(UpdateOrderDto dto, CancellationToken cancellationToken )
        {
            var result = await _sender.Send(new ChangeOrderStatusCommand(dto));
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

            return Ok(new { success = result.IsSuccess, message = "Order status Changed Successfully" });
        }

    }
}
