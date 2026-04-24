using Microsoft.EntityFrameworkCore;
using AwladRizk.Domain.Entities;
using AwladRizk.Domain.Interfaces;

namespace AwladRizk.Persistence.Repositories;

public class OfferRepository : GenericRepository<Offer>, IOfferRepository
{
    public OfferRepository(AwladRizkDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Offer>> GetActiveAsync(CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;
        return await _dbSet
            .AsNoTracking()
            .Where(o => o.IsActive && o.ValidUntil >= now)
            .OrderBy(o => o.ValidUntil)
            .ToListAsync(ct);
    }
}
