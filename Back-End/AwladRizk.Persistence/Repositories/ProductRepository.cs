using Microsoft.EntityFrameworkCore;
using AwladRizk.Domain.Entities;
using AwladRizk.Domain.Interfaces;

namespace AwladRizk.Persistence.Repositories;

/// <summary>
/// Product-specific repository with optimistic concurrency for stock deduction.
/// </summary>
public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AwladRizkDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Product>> GetFeaturedAsync(int count, CancellationToken ct = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(p => p.Category)
            .Where(p => p.IsOnSale && p.StockQty > 0)
            .OrderByDescending(p => p.CreatedAt)
            .Take(count)
            .ToListAsync(ct);
    }

    public async Task<Product?> GetWithCategoryByIdAsync(int id, CancellationToken ct = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public async Task<(IReadOnlyList<Product> Items, int TotalCount)> GetPagedAsync(
        string? categorySlug,
        string? search,
        bool? onSaleOnly,
        int page,
        int pageSize,
        CancellationToken ct = default)
    {
        var query = _dbSet
            .AsNoTracking()
            .Include(p => p.Category)
            .Where(p => p.StockQty > 0)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(categorySlug))
        {
            query = query.Where(p => p.Category.Slug == categorySlug);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(p => p.NameAr.Contains(term) || p.NameEn.Contains(term));
        }

        if (onSaleOnly == true)
        {
            query = query.Where(p => p.IsOnSale);
        }

        var totalCount = await query.CountAsync(ct);
        var items = await query
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return (items, totalCount);
    }

    public async Task<IReadOnlyDictionary<int, Product>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken ct = default)
    {
        var idList = ids.Distinct().ToList();
        return await _dbSet
            .Where(p => idList.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id, ct);
    }

    /// <summary>
    /// Attempts to deduct stock with optimistic concurrency.
    /// Loads the product, calls domain method, and saves.
    /// If a concurrent update occurred, DbUpdateConcurrencyException propagates.
    /// </summary>
    public async Task<bool> TryDeductStockAsync(int productId, int quantity, CancellationToken ct = default)
    {
        var product = await _dbSet.FindAsync(new object[] { productId }, ct);

        if (product is null)
            return false;

        if (!product.TryDeductStock(quantity))
            return false;

        // The concurrency check happens on SaveChangesAsync via RowVersion.
        // If another transaction modified this row, EF Core will throw
        // DbUpdateConcurrencyException — which the caller (command handler) handles.
        return true;
    }
}
