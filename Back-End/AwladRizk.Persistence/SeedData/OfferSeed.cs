using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AwladRizk.Domain.Entities;

namespace AwladRizk.Persistence.SeedData;

/// <summary>
/// Seeds the 6 offers matching the frontend offers page.
/// </summary>
public class OfferSeed : IEntityTypeConfiguration<Offer>
{
    private static readonly DateTime _seedDate = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private static readonly DateTime _validUntil = new(2026, 12, 31, 23, 59, 59, DateTimeKind.Utc);

    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.HasData(
            new Offer
            {
                Id = 1,
                TitleAr = "🔥 خصم 30% على شيبسي الأحجام الكبيرة",
                TitleEn = "🔥 30% Off Chipsy Large Bags",
                DescriptionAr = "خصم 30% على كل منتجات شيبسي",
                DescriptionEn = "30% off all Chipsy products",
                DiscountPercent = 30,
                BadgeText = "30%",
                Icon = "🔥",
                ValidUntil = _validUntil,
                IsActive = true,
                CreatedAt = _seedDate
            },
            new Offer
            {
                Id = 2,
                TitleAr = "🧃 عصير جهينة: اشترِ 3 واحصل على 1 مجاناً",
                TitleEn = "🧃 Juhayna Juice: Buy 3 Get 1 Free",
                DescriptionAr = "اشترِ 3 جهينة واحصل على 1 مجاناً",
                DescriptionEn = "Buy 3 Juhayna get 1 free",
                DiscountPercent = 25,
                BadgeText = "B3G1",
                Icon = "🧃",
                ValidUntil = _validUntil,
                IsActive = true,
                CreatedAt = _seedDate
            },
            new Offer
            {
                Id = 3,
                TitleAr = "☕ عرض نسكافيه: الكيس الكبير بـ ٨٥ جنيه بدلاً من ١٢٠",
                TitleEn = "☕ Nescafé Offer: Large Pack EGP 85 Instead of 120",
                DescriptionAr = "نسكافيه ٣ في ١ بـ ٨٥ جنيه بدلاً من ١٢٠",
                DescriptionEn = "Nescafé 3in1 EGP 85 instead of 120",
                DiscountPercent = 29,
                BadgeText = "٨٥ ج",
                Icon = "☕",
                ValidUntil = _validUntil,
                IsActive = true,
                CreatedAt = _seedDate
            },
            new Offer
            {
                Id = 4,
                TitleAr = "🧹 خصم 20% على كل المنظفات",
                TitleEn = "🧹 20% Off All Cleaning Products",
                DescriptionAr = "خصم 20% على كل المنظفات",
                DescriptionEn = "20% off all cleaning products",
                DiscountPercent = 20,
                BadgeText = "20%",
                Icon = "🧹",
                ValidUntil = _validUntil,
                IsActive = true,
                CreatedAt = _seedDate
            },
            new Offer
            {
                Id = 5,
                TitleAr = "☕ شاي العروسة ٢٠٠ كيس بـ ٩٥ جنيه فقط",
                TitleEn = "☕ El Arosa Tea 200 Bags for EGP 95 Only",
                DescriptionAr = "شاي العروسة ٢٠٠ كيس بـ ٩٥ جنيه فقط",
                DescriptionEn = "El Arosa Tea 200 bags for EGP 95 only",
                DiscountPercent = 24,
                BadgeText = "٩٥ ج",
                Icon = "☕",
                ValidUntil = _validUntil,
                IsActive = true,
                CreatedAt = _seedDate
            },
            new Offer
            {
                Id = 6,
                TitleAr = "🥫 فول كاليفورنيا بخصم ٢٥%",
                TitleEn = "🥫 California Garden Foul 25% Off",
                DescriptionAr = "فول كاليفورنيا بخصم ٢٥%",
                DescriptionEn = "California Garden Foul 25% off",
                DiscountPercent = 25,
                BadgeText = "25%",
                Icon = "🥫",
                ValidUntil = _validUntil,
                IsActive = true,
                CreatedAt = _seedDate
            }
        );
    }
}
