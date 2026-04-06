using ECommerceNew.Application.Abstractions;
using ECommerceNew.Infrastructure.EfCore;
using ECommerceNew.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceNew.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ECommerceDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository , UserRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IStorageRepoistory, StorageRepository>();
            services.AddTransient<IEmailService, SmtpEmailService>();  

            return services;
        }
    }
}
