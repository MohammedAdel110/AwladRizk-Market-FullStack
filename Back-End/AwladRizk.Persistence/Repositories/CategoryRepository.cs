using Microsoft.EntityFrameworkCore;
using AwladRizk.Domain.Entities;
using AwladRizk.Domain.Interfaces;

namespace AwladRizk.Persistence.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AwladRizkDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Category>> GetAllWithProductsAsync(CancellationToken ct = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(c => c.Products)
            .OrderBy(c => c.SortOrder)
            .ToListAsync(ct);
    }
}
