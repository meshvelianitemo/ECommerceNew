using ECommerceNew.Application.ProductCQRS.Commands.AddToWishList;
using ECommerceNew.Application.ProductCQRS.Commands.RemoveFromWishlist;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.ProductCQRS.Queries.GetWishList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ECommerceNew.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class WishlistController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ILogger<WishlistController> _logger;

    public WishlistController(ISender sender, ILogger<WishlistController> logger)
    {
        _sender = sender;
        _logger = logger;
    }
    
    
    [EnableRateLimiting("token")]
    [HttpGet]
    public async Task<ActionResult<WishListDetailDto>> GetWishlist([FromQuery] WishlistQueryParameters queryParams, CancellationToken cancellationToken)
    {
        
        var result = await _sender.Send(new GetWishListQuery(queryParams), cancellationToken);
        if(!result.IsSuccess)
        {
            return NotFound(new {
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
    public async Task<ActionResult> AddToWishlist([FromBody] AddToWishlistDto dto, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new AddToWishListCommand(dto), cancellationToken);
        if (!result.IsSuccess)
        {
            if (result.Error != null && result.Error.Code == "Product.AlreadyInWishlist")
            {
                return Conflict(new
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

            return BadRequest(new
            {
                success = false,
                error = new
                {
                    code = result.Error?.Code,
                    message = result.Error?.Message,
                    field = result.Error?.Field
                }
            });
        }

        return Ok(new { success = true, message = "Product Added to Wishlist." });
    }


    [EnableRateLimiting("sliding")]
    [HttpDelete("{productId}")]
    public async Task<ActionResult> RemoveFromWishlist([FromBody] RemoveFromWishlistDto dto, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new RemoveFromWishlistCommand(dto), cancellationToken);
        if (!result.IsSuccess)
        {
            return BadRequest(new
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
            message = "Successfully removed from wishlist."
        });
    }


}

