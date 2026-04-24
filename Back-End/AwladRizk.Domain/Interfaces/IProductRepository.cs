using AwladRizk.Domain.Entities;

namespace AwladRizk.Domain.Interfaces;

/// <summary>
/// Product-specific repository for write operations.
/// Handles stock deduction with optimistic concurrency.
/// </summary>
public interface IProductRepository : IRepository<Product>
{
    Task<IReadOnlyList<Product>> GetFeaturedAsync(int count, CancellationToken ct = default);
    Task<Product?> GetWithCategoryByIdAsync(int id, CancellationToken ct = default);
    Task<(IReadOnlyList<Product> Items, int TotalCount)> GetPagedAsync(
        string? categorySlug,
        string? search,
        bool? onSaleOnly,
        int page,
        int pageSize,
        CancellationToken ct = default);
    Task<IReadOnlyDictionary<int, Product>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken ct = default);

    /// <summary>
    /// Attempts to deduct stock for a product.
    /// Throws DbUpdateConcurrencyException if a race condition is detected.
    /// </summary>
    Task<bool> TryDeductStockAsync(int productId, int quantity, CancellationToken ct = default);
}
