using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using AwladRizk.API.Middleware;
using AwladRizk.API.Hubs;
using AwladRizk.Application;
using AwladRizk.Domain.Entities;
using AwladRizk.Infrastructure;
using AwladRizk.Persistence;

var builder = WebApplication.CreateBuilder(args);

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
    ?? ["http://localhost:3000", "http://localhost:5173", "http://localhost:5500", "http://127.0.0.1:5500"];

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AwladRizk API",
        Version = "v1",
        Description = "Backend API for AwladRizk e-commerce services."
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token as: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("ClientPolicy", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            // Allow LAN access during development (e.g. http://192.168.x.x:5500).
            policy.SetIsOriginAllowed(_ => true)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        }
        else
        {
            policy.WithOrigins(allowedOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        }
    });
});

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "AwladRizk.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.None;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.Use(async (context, next) =>
{
    context.Response.Headers.TryAdd("X-Content-Type-Options", "nosniff");
    context.Response.Headers.TryAdd("X-Frame-Options", "DENY");
    context.Response.Headers.TryAdd("Referrer-Policy", "no-referrer");
    await next();
});
app.UseStaticFiles();
app.UseCors("ClientPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.UseMiddleware<SessionCookieMiddleware>();
app.MapControllers();
app.MapHub<OrderHub>("/orderHub");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AwladRizkDbContext>();
    if (app.Environment.IsDevelopment())
    {
        var admin = await db.AdminUsers.FirstOrDefaultAsync(a => a.Email == "admin@awladrizk.com");
        if (admin is null)
        {
            db.AdminUsers.Add(new AdminUser
            {
                FullName = "Admin",
                Email = "admin@awladrizk.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                Role = "Admin",
                CreatedAt = DateTime.UtcNow
            });
            await db.SaveChangesAsync();
        }
        else
        {
            admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123");
            admin.Role = "Admin";
            db.AdminUsers.Update(admin);
            await db.SaveChangesAsync();
        }
    }
}

app.Run();
