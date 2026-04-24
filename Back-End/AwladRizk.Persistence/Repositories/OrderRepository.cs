using Microsoft.EntityFrameworkCore;
using AwladRizk.Domain.Entities;
using AwladRizk.Domain.Enums;
using AwladRizk.Domain.Interfaces;

namespace AwladRizk.Persistence.Repositories;

/// <summary>
/// Order-specific repository.
/// </summary>
public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(AwladRizkDbContext context) : base(context) { }

    public async Task<Order?> GetByOrderNumberAsync(string orderNumber, CancellationToken ct = default)
    {
        return await _dbSet
            .Include(o => o.Items)
            .Include(o => o.Payment)
            .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber, ct);
    }

    public async Task<Order?> GetByIdWithDetailsAsync(int id, CancellationToken ct = default)
    {
        return await _dbSet
            .Include(o => o.Items)
            .Include(o => o.Payment)
            .FirstOrDefaultAsync(o => o.Id == id, ct);
    }

    public async Task<(IReadOnlyList<Order> Items, int TotalCount)> GetAdminOrdersAsync(
        OrderStatus[]? statuses,
        int page,
        int pageSize,
        CancellationToken ct = default)
    {
        page = page <= 0 ? 1 : page;
        pageSize = pageSize <= 0 ? 20 : Math.Min(pageSize, 100);

        var q = _dbSet
            .AsNoTracking()
            .Include(o => o.Payment)
            .OrderByDescending(o => o.CreatedAt)
            .AsQueryable();

        if (statuses is { Length: > 0 })
        {
            q = q.Where(o => statuses.Contains(o.Status));
        }

        var total = await q.CountAsync(ct);
        var items = await q
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return (items, total);
    }
}
