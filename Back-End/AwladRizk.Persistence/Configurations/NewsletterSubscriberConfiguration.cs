using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AwladRizk.Domain.Entities;

namespace AwladRizk.Persistence.Configurations;

public class NewsletterSubscriberConfiguration : IEntityTypeConfiguration<NewsletterSubscriber>
{
    public void Configure(EntityTypeBuilder<NewsletterSubscriber> builder)
    {
        builder.ToTable("NewsletterSubscribers");

        builder.HasKey(n => n.Id);

        builder.Property(n => n.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(n => n.SubscribedAt)
            .IsRequired();

        builder.Property(n => n.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(n => n.Email).IsUnique();
    }
}
