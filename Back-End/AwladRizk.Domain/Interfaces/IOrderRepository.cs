using AwladRizk.Domain.Entities;

namespace AwladRizk.Domain.Interfaces;

/// <summary>
/// Order-specific repository for write operations.
/// </summary>
public interface IOrderRepository : IRepository<Order>
{
    Task<Order?> GetByOrderNumberAsync(string orderNumber, CancellationToken ct = default);
}
