using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AwladRizk.Domain.Entities;

namespace AwladRizk.Persistence.SeedData;

public class HomeTickerMessageSeed : IEntityTypeConfiguration<HomeTickerMessage>
{
    private static readonly DateTime SeedDate = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public void Configure(EntityTypeBuilder<HomeTickerMessage> builder)
    {
        builder.HasData(
            new HomeTickerMessage
            {
                Id = 1,
                TextAr = "🔥 خصم 30% على شيبسي الأحجام الكبيرة",
                TextEn = "🔥 30% Off Chipsy Large Bags",
                SortOrder = 1,
                IsActive = true,
                CreatedAt = SeedDate
            },
            new HomeTickerMessage
            {
                Id = 2,
                TextAr = "🧃 عصير جهينة: اشترِ 3 واحصل على 1 مجاناً",
                TextEn = "🧃 Juhayna Juice: Buy 3 Get 1 Free",
                SortOrder = 2,
                IsActive = true,
                CreatedAt = SeedDate
            },
            new HomeTickerMessage
            {
                Id = 3,
                TextAr = "🚚 توصيل مجاني للطلبات فوق 200 جنيه",
                TextEn = "🚚 Free delivery on orders over EGP 200",
                SortOrder = 3,
                IsActive = true,
                CreatedAt = SeedDate
            });
    }
}
