using Amazon.S3.Model;
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Orders.Commands.PlaceOrder;
using ECommerceNew.Application.Orders.DTOs;
using ECommerceNew.Application.Orders.Queries;
using ECommerceNew.Domain.enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceNew.Api.Controllers.Orders
{
    [Route("api/[controller]")]
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

        [HttpPut]
        public async Task<IActionResult> ChangeOrderStatus(OrderStatus newOrderStatus, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new ChangeOrderStatusCommand(, cancellationToken), cancellationToken);
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
