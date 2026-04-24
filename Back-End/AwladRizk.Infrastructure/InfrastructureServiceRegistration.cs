using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using AwladRizk.Domain.Interfaces;
using AwladRizk.Infrastructure.Auth;
using AwladRizk.Infrastructure.Cart;
using AwladRizk.Infrastructure.Payments;

namespace AwladRizk.Infrastructure;

/// <summary>
/// DI registration extension for the Infrastructure layer.
/// Call this from Program.cs: builder.Services.AddInfrastructureServices(configuration);
/// </summary>
public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ─── Redis Distributed Cache ───────────────────────────────────
        var redisConnection = configuration.GetConnectionString("Redis");
        if (!string.IsNullOrEmpty(redisConnection))
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnection;
                options.InstanceName = "AwladRizk:";
            });
        }
        else
        {
            // Fallback to in-memory cache for development without Redis
            services.AddDistributedMemoryCache();
        }

        // ─── Cart Services ─────────────────────────────────────────────
        services.AddScoped<ICartService, RedisCartService>();
        services.AddScoped<CartEnrichmentService>();

        // ─── Payment Gateway ───────────────────────────────────────────
        services.AddScoped<IPaymentGateway, MockPaymentGateway>();

        // ─── JWT Token Service ─────────────────────────────────────────
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        // ─── JWT Authentication ────────────────────────────────────────
        var jwtSettings = configuration.GetSection("Jwt");
        var key = jwtSettings["Key"];

        if (!string.IsNullOrEmpty(key))
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"] ?? "AwladRizk.API",
                    ValidAudience = jwtSettings["Audience"] ?? "AwladRizk.Client",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });
        }

        services.AddAuthorization();

        return services;
    }
}
