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
