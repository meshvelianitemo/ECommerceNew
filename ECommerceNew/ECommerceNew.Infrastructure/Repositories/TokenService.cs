using ECommerceNew.Application.Abstractions;
using ECommerceNew.Domain.Entities.UserSide;
using ECommerceNew.Infrastructure.EfCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerceNew.Infrastructure.Repositories
{
    public class TokenService : ITokenService
    {
        
        private readonly IConfiguration _config;
        private readonly ILogger<TokenService> _logger;
        public TokenService( IConfiguration config, ILogger<TokenService> logger)
        {
            _logger = logger;
            _config = config;

        }

        public async Task<string> GenerateTokenAsync(User user, CancellationToken cancellationToken = default)
        {
            var roleName = (UserRolesEnum)user.RoleId;
            _logger.LogInformation("Generating token for user {UserId} with role {Role}", user.UserId, roleName);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, roleName.ToString()),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName)
            };

            var token = new JwtSecurityToken(

                   issuer: _config["JwtSettings:validIssuer"],
                   audience: _config["JwtSettings:validAudience"],
                   claims: claims,
                   expires: DateTime.UtcNow.AddMinutes(30),
                   signingCredentials: creds

                   );
            _logger.LogInformation("Token generated for user {UserId} with role {Role}", user.UserId, roleName);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return await Task.FromResult(jwt);
        }

    }
}
