using DotNetEnv;
using ECommerceNew.Application;
using ECommerceNew.Application.Responses.Exceptions;
using ECommerceNew.Infrastructure;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PaypalServerSdk.Standard;
using PaypalServerSdk.Standard.Authentication;
using Serilog;
using System.Security.Claims;
using System.Threading.RateLimiting;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.Sources.Clear();
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true)
    .AddJsonFile("appsettings.Development.json")
    .AddEnvironmentVariables(); 

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddPolicy("fixed", httpContext =>
    RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.Connection
    .RemoteIpAddress?.ToString() ?? "unknown", factory: partition => new FixedWindowRateLimiterOptions
    {
        Window = TimeSpan.FromSeconds(10),
        PermitLimit = 3,
        QueueLimit = 0,
        QueueProcessingOrder = QueueProcessingOrder.OldestFirst
    }));

    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    rateLimiterOptions.AddSlidingWindowLimiter("sliding", options =>
    {
        options.Window = TimeSpan.FromSeconds(15);
        options.PermitLimit = 20;
        options.SegmentsPerWindow = 10;
        options.QueueLimit = 0;
    });

    rateLimiterOptions.AddTokenBucketLimiter("token", options =>
    {
        options.TokenLimit = 300;
        options.TokensPerPeriod = 30;
        options.ReplenishmentPeriod = TimeSpan.FromSeconds(5);
        options.QueueLimit = 0;
    });

    rateLimiterOptions.AddConcurrencyLimiter("concurrency", options =>
    {
        options.PermitLimit = 30;
        options.QueueLimit = 0;
    });
});

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddAuthentication(x =>
{
x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtSettings:validIssuer"],
        ValidAudience = builder.Configuration["JwtSettings:validAudience"],
        IssuerSigningKey = new SymmetricSecurityKey
           (System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        RoleClaimType = ClaimTypes.Role
    };
});



var client = new PaypalServerSdkClient.Builder()
    .ClientCredentialsAuth(
        new ClientCredentialsAuthModel.Builder(
            "YOUR_CLIENT_ID",
            "YOUR_CLIENT_SECRET"
        ).Build()
    )
    .Environment(PaypalServerSdk.Standard.Environment.Sandbox) // or Production
    .Build();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .WithOrigins(
                "http://localhost:8080",
                "http://127.0.0.1:8080",
                "http://localhost:5500",
                "http://localhost:3000",   
                "http://127.0.0.1:5500",
                "http://127.0.0.1:5501",
                "null"                    
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());         
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseRateLimiter();
app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
