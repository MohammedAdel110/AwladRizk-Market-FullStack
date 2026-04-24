using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AwladRizk.Domain.Entities;

namespace AwladRizk.Persistence.SeedData;

/// <summary>
/// Seeds the 8 product categories matching the frontend.
/// </summary>
public class CategorySeed : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(
            new Category { Id = 1, NameAr = "شيبسي ومقرمشات", NameEn = "Chips & Snacks", Slug = "chips-snacks", Icon = "🍿", SortOrder = 1, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Category { Id = 2, NameAr = "عصائر ومشروبات", NameEn = "Juices & Drinks", Slug = "juices-drinks", Icon = "🧃", SortOrder = 2, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Category { Id = 3, NameAr = "قهوة وشاي", NameEn = "Coffee & Tea", Slug = "coffee-tea", Icon = "☕", SortOrder = 3, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Category { Id = 4, NameAr = "ألبان وأجبان", NameEn = "Dairy & Cheese", Slug = "dairy-cheese", Icon = "🧀", SortOrder = 4, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Category { Id = 5, NameAr = "بسكويت وحلويات", NameEn = "Biscuits & Sweets", Slug = "biscuits-sweets", Icon = "🍪", SortOrder = 5, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Category { Id = 6, NameAr = "معلبات", NameEn = "Canned Food", Slug = "canned-food", Icon = "🥫", SortOrder = 6, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Category { Id = 7, NameAr = "منظفات", NameEn = "Cleaning", Slug = "cleaning", Icon = "🧹", SortOrder = 7, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Category { Id = 8, NameAr = "معكرونة وأرز", NameEn = "Pasta & Rice", Slug = "pasta-rice", Icon = "🍝", SortOrder = 8, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
