using AwladRizk.Domain.Entities;

namespace AwladRizk.Domain.Interfaces;

public interface INewsletterSubscriberRepository : IRepository<NewsletterSubscriber>
{
    Task<NewsletterSubscriber?> GetByEmailAsync(string email, CancellationToken ct = default);
}
