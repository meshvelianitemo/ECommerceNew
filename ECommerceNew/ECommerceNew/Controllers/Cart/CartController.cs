using ECommerceNew.Application.ProductCQRS.Commands.AddToCart;
using ECommerceNew.Application.ProductCQRS.Commands.RemoveFromCart;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.ProductCQRS.Queries.GetCartItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ECommerceNew.Api.Controllers.Cart
{

    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ISender _sender;
        public CartController(ISender sender)
        {
            _sender = sender;
        }


        [EnableRateLimiting("token")]
        [HttpGet]
        public async Task<ActionResult<CartItemDetailDto>> GetCartItems([FromQuery] CartItemsQueryParameters queryParams, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetCartItemsQuery(queryParams),
                cancellationToken);
            if (!result.IsSuccess)
            {
                return NotFound(new
                {
                    success = false,
                    error = new
                    {
                        code = result.Error.Code,
                        message = result.Error.Message,
                        field = result.Error.Field
                    }
                });
            }
            return Ok(new { success = true, result.Value });
        }

      
        [EnableRateLimiting("sliding")]
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto dto, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new AddToCartCommand(dto),
                cancellationToken);
            if (!result.IsSuccess)
            {
                return NotFound(new
                {
                    success = false,
                    error = new
                    {
                        code = result.Error.Code,
                        message = result.Error.Message,
                        field = result.Error.Field
                    }
                });
            }
            return Ok(new { success = true, message = "Item added to cart successfully." });
        }

        [Authorize]
        [EnableRateLimiting("sliding")]
        [HttpDelete("{productId}")]
        public async Task<ActionResult> RemoveFromCart([FromBody] RemoveFromCartDto dto, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new RemoveFromCartCommand(dto), cancellationToken);
            if (!result.IsSuccess)
            {
                return NotFound(new
                {
                    success = false,
                    error = new
                    {
                        code = result.Error.Code,
                        message = result.Error.Message,
                        field = result.Error.Field
                    }
                });
            }
            return Ok(new
            {
                success = true,
                message = "Successfully removed from cart."
            });
        }
    }
}
