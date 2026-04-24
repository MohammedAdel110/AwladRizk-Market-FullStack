using Microsoft.EntityFrameworkCore;
using AwladRizk.Domain.Entities;
using AwladRizk.Domain.Interfaces;

namespace AwladRizk.Persistence.Repositories;

public class NewsletterSubscriberRepository : GenericRepository<NewsletterSubscriber>, INewsletterSubscriberRepository
{
    public NewsletterSubscriberRepository(AwladRizkDbContext context) : base(context) { }

    public async Task<NewsletterSubscriber?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        return await _dbSet.FirstOrDefaultAsync(
            n => n.Email.ToLower() == email.ToLower(),
            ct);
    }
}
