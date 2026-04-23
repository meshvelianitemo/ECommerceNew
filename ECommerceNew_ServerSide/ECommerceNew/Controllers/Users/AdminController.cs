using ECommerceNew.Application.Auth.DTOs;
using ECommerceNew.Application.Auth.Queries.GetUsers;
using ECommerceNew.Application.Product.Commands.CreateProduct;
using ECommerceNew.Application.Product.DTOs.ProductDtos;
using ECommerceNew.Application.ProductCQRS.Commands.DeleteProduct;
using ECommerceNew.Application.ProductCQRS.Commands.DeleteProductImage;
using ECommerceNew.Application.ProductCQRS.Commands.UpdateProduct;
using ECommerceNew.Application.ProductCQRS.Commands.UploadImage;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ECommerceNew.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [EnableRateLimiting("concurrency")]
    [Route("api/[controller]")]
    public class AdminUsersController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<AdminUsersController> _logger;
        public AdminUsersController(ISender sender, ILogger<AdminUsersController> logger)
        {
            _logger = logger;
            _sender = sender;
        }

        [HttpGet("Users")]
        public async Task<IActionResult> GetUsers([FromQuery] UserQueryParameters parameters, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetUsersQuery(parameters), cancellationToken);
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
            return Ok(new { success = result.IsSuccess, result.Value });
        }

    }
}
