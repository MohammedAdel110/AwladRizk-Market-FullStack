using AwladRizk.Domain.Entities;
using AwladRizk.Domain.Enums;

namespace AwladRizk.Domain.Interfaces;

/// <summary>
/// Order-specific repository for write operations.
/// </summary>
public interface IOrderRepository : IRepository<Order>
{
    Task<Order?> GetByOrderNumberAsync(string orderNumber, CancellationToken ct = default);
    Task<Order?> GetByIdWithDetailsAsync(int id, CancellationToken ct = default);
    Task<(IReadOnlyList<Order> Items, int TotalCount)> GetAdminOrdersAsync(
        OrderStatus[]? statuses,
        int page,
        int pageSize,
        CancellationToken ct = default);
}
