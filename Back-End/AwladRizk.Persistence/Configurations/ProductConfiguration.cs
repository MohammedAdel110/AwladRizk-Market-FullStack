using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AwladRizk.Domain.Entities;

namespace AwladRizk.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.NameAr)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.NameEn)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.OldPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.ImageUrl)
            .HasMaxLength(500);

        builder.Property(p => p.StockQty)
            .IsRequired()
            .HasDefaultValue(0);

        // Optimistic concurrency token
        builder.Property(p => p.RowVersion)
            .IsRowVersion();

        // FK to Category
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(p => p.CategoryId);
        builder.HasIndex(p => p.IsOnSale);
    }
}
