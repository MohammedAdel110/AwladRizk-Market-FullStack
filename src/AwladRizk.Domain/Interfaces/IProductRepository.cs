using AwladRizk.Domain.Entities;

namespace AwladRizk.Domain.Interfaces;

/// <summary>
/// Product-specific repository for write operations.
/// Handles stock deduction with optimistic concurrency.
/// </summary>
public interface IProductRepository : IRepository<Product>
{
    /// <summary>
    /// Attempts to deduct stock for a product.
    /// Throws DbUpdateConcurrencyException if a race condition is detected.
    /// </summary>
    Task<bool> TryDeductStockAsync(int productId, int quantity, CancellationToken ct = default);
}
