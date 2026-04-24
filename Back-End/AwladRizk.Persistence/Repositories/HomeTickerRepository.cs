using Microsoft.EntityFrameworkCore;
using AwladRizk.Domain.Entities;
using AwladRizk.Domain.Interfaces;

namespace AwladRizk.Persistence.Repositories;

public class HomeTickerRepository : GenericRepository<HomeTickerMessage>, IHomeTickerRepository
{
    public HomeTickerRepository(AwladRizkDbContext context) : base(context) { }

    public async Task<IReadOnlyList<HomeTickerMessage>> GetAllOrderedAsync(CancellationToken ct = default)
    {
        return await _dbSet.AsNoTracking().OrderBy(x => x.SortOrder).ThenBy(x => x.Id).ToListAsync(ct);
    }

    public async Task<IReadOnlyList<HomeTickerMessage>> GetActiveOrderedAsync(CancellationToken ct = default)
    {
        return await _dbSet.AsNoTracking()
            .Where(x => x.IsActive)
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.Id)
            .ToListAsync(ct);
    }
}
