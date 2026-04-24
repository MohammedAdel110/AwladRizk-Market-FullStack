using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AwladRizk.Domain.Entities;

namespace AwladRizk.Persistence.SeedData;

/// <summary>
/// Seeds the 12 products matching the frontend prices exactly.
/// All prices are in EGP and match the TRANSLATIONS object in script.js.
/// </summary>
public class ProductSeed : IEntityTypeConfiguration<Product>
{
    private static readonly DateTime _seedDate = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(
            // p1: Chipsy Cheese — 25 EGP (old: 35)
            new Product
            {
                Id = 1,
                NameAr = "شيبسي كبيرة - بالجبنة",
                NameEn = "Chipsy Large - Cheese Flavor",
                Price = 25m,
                OldPrice = 35m,
                ImageUrl = "/images/chipsy-cheese.png",
                CategoryId = 1, // Chips & Snacks
                IsOnSale = true,
                StockQty = 100,
                CreatedAt = _seedDate
            },

            // p2: Juhayna Mango — 18 EGP
            new Product
            {
                Id = 2,
                NameAr = "عصير جهينة كلاسيك - مانجو",
                NameEn = "Juhayna Classic Juice - Mango",
                Price = 18m,
                OldPrice = null,
                ImageUrl = "/images/juhayna-mango.png",
                CategoryId = 2, // Juices & Drinks
                IsOnSale = false,
                StockQty = 200,
                CreatedAt = _seedDate
            },

            // p3: Nescafé 3in1 — 85 EGP (old: 120)
            new Product
            {
                Id = 3,
                NameAr = "نسكافيه ٣ في ١ - ٣٠ كيس",
                NameEn = "Nescafé 3in1 - 30 Sachets",
                Price = 85m,
                OldPrice = 120m,
                ImageUrl = "/images/nescafe-3in1.png",
                CategoryId = 3, // Coffee & Tea
                IsOnSale = true,
                StockQty = 80,
                CreatedAt = _seedDate
            },

            // p4: Domty Cheese — 42 EGP
            new Product
            {
                Id = 4,
                NameAr = "جبنة دومتي أبيض مثلثات",
                NameEn = "Domty White Cheese Triangles",
                Price = 42m,
                OldPrice = null,
                ImageUrl = "/images/domty-cheese.png",
                CategoryId = 4, // Dairy & Cheese
                IsOnSale = false,
                StockQty = 150,
                CreatedAt = _seedDate
            },

            // p5: El Arosa Tea — 95 EGP (old: 125)
            new Product
            {
                Id = 5,
                NameAr = "شاي العروسة - ٢٠٠ كيس",
                NameEn = "El Arosa Tea - 200 Bags",
                Price = 95m,
                OldPrice = 125m,
                ImageUrl = "/images/arosa-tea.png",
                CategoryId = 3, // Coffee & Tea
                IsOnSale = true,
                StockQty = 60,
                CreatedAt = _seedDate
            },

            // p6: Molto Croissant — 60 EGP
            new Product
            {
                Id = 6,
                NameAr = "مولتو كرواسون - ١٢ قطعة",
                NameEn = "Molto Croissant - 12 Pack",
                Price = 60m,
                OldPrice = null,
                ImageUrl = "/images/molto-croissant.png",
                CategoryId = 5, // Biscuits & Sweets
                IsOnSale = false,
                StockQty = 120,
                CreatedAt = _seedDate
            },

            // p7: Chipsy Ketchup — 15 EGP
            new Product
            {
                Id = 7,
                NameAr = "شيبسي بالكاتشب",
                NameEn = "Chipsy - Ketchup Flavor",
                Price = 15m,
                OldPrice = null,
                ImageUrl = "/images/chipsy-ketchup.png",
                CategoryId = 1, // Chips & Snacks
                IsOnSale = false,
                StockQty = 180,
                CreatedAt = _seedDate
            },

            // p8: Beyti Guava — 12 EGP
            new Product
            {
                Id = 8,
                NameAr = "عصير بيتي - جوافة",
                NameEn = "Beyti Juice - Guava",
                Price = 12m,
                OldPrice = null,
                ImageUrl = "/images/beyti-guava.png",
                CategoryId = 2, // Juices & Drinks
                IsOnSale = false,
                StockQty = 200,
                CreatedAt = _seedDate
            },

            // p9: California Garden Foul — 28 EGP (old: 38)
            new Product
            {
                Id = 9,
                NameAr = "فول كاليفورنيا جاردن",
                NameEn = "California Garden Foul",
                Price = 28m,
                OldPrice = 38m,
                ImageUrl = "/images/california-foul.png",
                CategoryId = 6, // Canned Food
                IsOnSale = true,
                StockQty = 90,
                CreatedAt = _seedDate
            },

            // p10: Gena Tuna — 35 EGP
            new Product
            {
                Id = 10,
                NameAr = "تونة جنة",
                NameEn = "Gena Tuna",
                Price = 35m,
                OldPrice = null,
                ImageUrl = "/images/gena-tuna.png",
                CategoryId = 6, // Canned Food
                IsOnSale = false,
                StockQty = 110,
                CreatedAt = _seedDate
            },

            // p11: Regina Pasta — 22 EGP
            new Product
            {
                Id = 11,
                NameAr = "مكرونة ريجينا - ٥٠٠ جرام",
                NameEn = "Regina Pasta - 500g",
                Price = 22m,
                OldPrice = null,
                ImageUrl = "/images/regina-pasta.png",
                CategoryId = 8, // Pasta & Rice
                IsOnSale = false,
                StockQty = 140,
                CreatedAt = _seedDate
            },

            // p12: Persil Detergent — 155 EGP (old: 190)
            new Product
            {
                Id = 12,
                NameAr = "برسيل غسيل - ٤ كيلو",
                NameEn = "Persil Detergent - 4kg",
                Price = 155m,
                OldPrice = 190m,
                ImageUrl = "/images/persil-detergent.png",
                CategoryId = 7, // Cleaning
                IsOnSale = true,
                StockQty = 50,
                CreatedAt = _seedDate
            }
        );
    }
}
