using ECommerceNew.Domain.Entities.UserSide;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;


namespace ECommerceNew.Infrastructure
{
    public class IdentitySeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public IdentitySeeder(UserManager<User> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task SeedAdminAsync()
        {
            var email = _config["AdminUser:Email"];
            var password = _config["AdminUser:Password"];

            var existing = await _userManager.FindByEmailAsync(email);
            if (existing != null) return;

            var user = new User
            {
                FirstName = "System",
                LastName = "Administrator",
                Email = email,
                PasswordHash = password,
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1),
                UpdatedAt = null,
                RoleId = 2
            };

            await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
