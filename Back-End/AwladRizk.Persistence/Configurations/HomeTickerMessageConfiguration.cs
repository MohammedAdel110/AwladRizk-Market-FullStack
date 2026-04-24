using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AwladRizk.Domain.Entities;

namespace AwladRizk.Persistence.Configurations;

public class HomeTickerMessageConfiguration : IEntityTypeConfiguration<HomeTickerMessage>
{
    public void Configure(EntityTypeBuilder<HomeTickerMessage> builder)
    {
        builder.ToTable("HomeTickerMessages");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.TextAr).IsRequired().HasMaxLength(300);
        builder.Property(x => x.TextEn).IsRequired().HasMaxLength(300);
        builder.Property(x => x.SortOrder).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.HasIndex(x => x.IsActive);
        builder.HasIndex(x => x.SortOrder);
    }
}
