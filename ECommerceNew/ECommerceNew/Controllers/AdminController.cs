using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Auth.Commands.EmailVerification;
using ECommerceNew.Application.Auth.Commands.Login;
using ECommerceNew.Application.Auth.Commands.PasswordRecovery;
using ECommerceNew.Application.Auth.Commands.PasswordRecoveryCode;
using ECommerceNew.Application.Auth.Commands.UserRegister;
using ECommerceNew.Application.Auth.Commands.VerifyRecoveryCode;
using ECommerceNew.Application.Auth.DTOs;
using ECommerceNew.Application.Auth.Queries.GetUsers;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.ProductCQRS.Queries.GetAllProducts;
using ECommerceNew.Application.Results.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ECommerceNew.Api.Controllers
{
    [ApiController]
    [EnableRateLimiting("fixed")]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ISender _sender;
        public AdminController(ISender sender)
        {
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
            return Ok(new { success = result.IsSuccess , result.Value });
        }
    }
}
