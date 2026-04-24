using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AwladRizk.Domain.Entities;

namespace AwladRizk.Persistence.Configurations;

public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.ToTable("Offers");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.TitleAr)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(o => o.TitleEn)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(o => o.DescriptionAr)
            .HasMaxLength(500);

        builder.Property(o => o.DescriptionEn)
            .HasMaxLength(500);

        builder.Property(o => o.DiscountPercent)
            .IsRequired();

        builder.Property(o => o.BadgeText)
            .HasMaxLength(50);

        builder.Property(o => o.Icon)
            .HasMaxLength(50);

        builder.Property(o => o.ValidUntil)
            .IsRequired();

        builder.Property(o => o.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(o => o.IsActive);
    }
}
