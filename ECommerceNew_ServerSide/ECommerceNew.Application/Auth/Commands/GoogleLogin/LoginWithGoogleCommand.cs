
using Amazon.Runtime.Internal;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace ECommerceNew.Application.Auth.Commands.GoogleLogin
{
    public class LoginWithGoogleCommand : IRequest<Task>
    {
        public ClaimsPrincipal? ClaimsPrincipal { get; set; }
        public LoginWithGoogleCommand(ClaimsPrincipal? claimsPrincipal)
        {
            ClaimsPrincipal = claimsPrincipal;
        }
    }
}
