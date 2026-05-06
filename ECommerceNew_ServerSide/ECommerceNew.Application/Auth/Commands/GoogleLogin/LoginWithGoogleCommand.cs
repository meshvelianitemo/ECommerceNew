
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Results.Errors;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace ECommerceNew.Application.Auth.Commands.GoogleLogin
{
    public class LoginWithGoogleCommand : IRequest<Result<string>>
    {
        public ClaimsPrincipal? ClaimsPrincipal { get; set; }
        public LoginWithGoogleCommand(ClaimsPrincipal? claimsPrincipal)
        {
            ClaimsPrincipal = claimsPrincipal;
        }
    }
}
