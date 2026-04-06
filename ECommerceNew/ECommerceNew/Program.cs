using ECommerceNew.Application;
using ECommerceNew.Application.Exceptions;
using ECommerceNew.Infrastructure;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Security.Claims;
using DotNetEnv;

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
app.UseAuthorization();

//app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
