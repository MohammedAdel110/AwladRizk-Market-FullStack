using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AwladRizk.Domain.Entities;
using AwladRizk.Domain.Enums;

namespace AwladRizk.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.OrderNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(o => o.SessionId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(o => o.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(30)
            .HasDefaultValue(OrderStatus.Pending);

        builder.Property(o => o.SubTotal)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(o => o.DeliveryFee)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(o => o.GrandTotal)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        // Delivery address stored as flattened columns
        builder.Property(o => o.DeliveryStreet).HasMaxLength(300);
        builder.Property(o => o.DeliveryArea).HasMaxLength(200);
        builder.Property(o => o.DeliveryCity).HasMaxLength(100);
        builder.Property(o => o.DeliveryGovernorate).HasMaxLength(100);

        builder.Property(o => o.Phone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(o => o.Notes)
            .HasMaxLength(500);

        // Indexes
        builder.HasIndex(o => o.OrderNumber).IsUnique();
        builder.HasIndex(o => o.SessionId);
        builder.HasIndex(o => o.Status);
    }
}
