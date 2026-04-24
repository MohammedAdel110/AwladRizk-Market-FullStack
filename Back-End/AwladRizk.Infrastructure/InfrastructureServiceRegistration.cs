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
        // Supports both:
        // - JwtTokenSettings:SecretKey/Issuer/Audience
        // - Jwt:Key/Issuer/Audience (legacy)
        var jwtSettings = configuration.GetSection("JwtTokenSettings");
        var key = jwtSettings["SecretKey"];

        if (string.IsNullOrWhiteSpace(key))
        {
            jwtSettings = configuration.GetSection("Jwt");
            key = jwtSettings["Key"];
        }

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

                // Allow JWTs to be passed via query string for SignalR WebSocket connections.
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/orderHub"))
                        {
                            context.Token = accessToken!;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }

        services.AddAuthorization();

        return services;
    }
}
