using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AwladRizk.Domain.Entities;

namespace AwladRizk.Persistence.SeedData;

/// <summary>
/// Seeds the default admin user.
/// Password is pre-hashed using BCrypt for "Admin@123".
/// In production, rotate this immediately after first login.
/// </summary>
public class AdminUserSeed : IEntityTypeConfiguration<AdminUser>
{
    public void Configure(EntityTypeBuilder<AdminUser> builder)
    {
        // BCrypt hash of "Admin@123" (cost factor 12)
        // Generated via: BCrypt.Net.BCrypt.HashPassword("Admin@123", 12)
        const string hashedPassword = "$2a$12$LJ3m4ys5LlGGMr6VJR8X7OE4J.K3FZkBGDMHgJHGQ3T5jR5Yn2mVe";

        builder.HasData(
            new AdminUser
            {
                Id = 1,
                FullName = "Admin",
                Email = "admin@awladrizk.com",
                PasswordHash = hashedPassword,
                Role = "Admin",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
