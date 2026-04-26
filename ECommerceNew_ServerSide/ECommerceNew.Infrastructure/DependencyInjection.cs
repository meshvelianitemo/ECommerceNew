using ECommerceNew.Application.Abstractions;
using ECommerceNew.Infrastructure.EfCore;
using ECommerceNew.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaypalServerSdk.Standard;
using PaypalServerSdk.Standard.Authentication;

namespace ECommerceNew.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            services.AddDbContext<ECommerceDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository , UserRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IStorageRepoistory, StorageRepository>();
            services.AddTransient<IEmailService, SmtpEmailService>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddSingleton(sp =>
            {
                var config = sp.GetRequiredService<Microsoft.Extensions.Configuration.IConfiguration>();
                return new PaypalServerSdkClient.Builder()
                    .ClientCredentialsAuth(
                        new ClientCredentialsAuthModel.Builder(
                            config["PayPal:ClientId"],
                            config["PayPal:ClientSecret"]
                        ).Build()
                    )
                    .Environment(PaypalServerSdk.Standard.Environment.Sandbox)
                    .Build();
            });

            return services;
        }
    }
}
