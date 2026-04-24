using Microsoft.EntityFrameworkCore;
using AwladRizk.Domain.Entities;
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
}
