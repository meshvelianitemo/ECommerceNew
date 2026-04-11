using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Auth.Commands.EmailVerification;
using ECommerceNew.Application.Auth.Commands.Login;
using ECommerceNew.Application.Auth.Commands.PasswordRecovery;
using ECommerceNew.Application.Auth.Commands.PasswordRecoveryCode;
using ECommerceNew.Application.Auth.Commands.UserRegister;
using ECommerceNew.Application.Auth.Commands.VerifyRecoveryCode;
using ECommerceNew.Application.Auth.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceNew.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ITokenService _tokenService;
        public AuthenticationController(ISender sender, ITokenService tokenService)
        {
            _sender = sender;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new LoginCommand(request), cancellationToken);

            if (!result.IsSuccess)
            {
                return Unauthorized(new { success = false,
                    error = new
                    {
                        code = result.Error.Code,
                        message = result.Error.Message,
                        field = result.Error.Field
                    }
                });
            }

            var webToken = await _tokenService.GenerateTokenAsync(result.Value, cancellationToken);

            Response.Cookies.Append("jwt", webToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(30)
            });

            return Ok(new { success = true, token = webToken });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new RegisterCommand(request), cancellationToken);
            if (!result.IsSuccess)
            {
                return BadRequest(new { success = false,
                    error = new
                    {
                        code = result.Error.Code,
                        message = result.Error.Message,
                        field = result.Error.Field
                    }
                });
            }
            return Ok(new { success = true , message = "To complete registration proceed to Email verification." , userId = result.Value.UserId });
        }

        [HttpPost("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail([FromBody] EmailVerificationDto request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new VerificationCommand(request), cancellationToken);

            if (!result.IsSuccess)
            {
                return result.Error.Code switch
                {
                    "User.InvalidVerificationCode" => BadRequest(new {success= false,
                        error = new
                        {
                            code = result.Error.Code,
                            message = result.Error.Message,
                            field = result.Error.Field
                        }
                    }),
                    "User.VerificationCodeAlreadyUsed" => Conflict(new { success = false,
                        error = new
                        {
                            code = result.Error.Code,
                            message = result.Error.Message,
                            field = result.Error.Field
                        }
                    }),
                    "User.ExpiredVerificationCode" => BadRequest(new { success = false,
                        error = new
                        {
                            code = result.Error.Code,
                            message = result.Error.Message,
                            field = result.Error.Field
                        }
                    }),
                    _ => StatusCode(500)
                };
            }
            return Ok(new {success = true, message = "Email verified successfully!" });
        }

        [HttpPost("SendPasswordRecovery")]
        public async Task<IActionResult> SendPasswordRecovery([FromBody] PasswordRecoveryEmailDto request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new PasswordRecoveryCommand(request), cancellationToken);
            if (!result.IsSuccess)
            {
                return BadRequest(new { success = false ,
                    error = new
                    {
                        code = result.Error.Code,
                        message = result.Error.Message,
                        field = result.Error.Field
                    }
                });
            }
            return Ok(new { success = true , message = "Password recovery email sent successfully!" });

        }

        [HttpPost("VerifyPasswordRecoveryCode")]
        public async Task<IActionResult> VerifyPasswordRecoveryCode([FromBody] RecoveryCodeVerificationDto request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new VerifyRecoveryCodeCommand(request), cancellationToken);
            if (!result.IsSuccess)
            {
                return BadRequest(new { success = false ,
                    error = new
                    {
                        code = result.Error.Code,
                        message = result.Error.Message,
                        field = result.Error.Field
                    }
                });
            }
            return Ok(new { success = true, message = "Verification code is valid!" });
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordResetDto request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new PasswordResetCommand(request), cancellationToken);
            if (!result.IsSuccess)
            {
                return BadRequest(new { success = false,
                    error = new
                    {
                        code = result.Error.Code,
                        message = result.Error.Message,
                        field = result.Error.Field
                    }
                });
            }
            return Ok(new { success = true, message = "Password reset successfully!" });
        }

        
    }
}
