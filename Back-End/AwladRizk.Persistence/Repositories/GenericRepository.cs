using Microsoft.EntityFrameworkCore;
using AwladRizk.Domain.Common;
using AwladRizk.Domain.Interfaces;

namespace AwladRizk.Persistence.Repositories;

/// <summary>
/// Generic repository implementation for WRITE operations.
/// Read operations bypass this — they use DbContext directly with AsNoTracking().
/// </summary>
public class GenericRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly AwladRizkDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(AwladRizkDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, ct);
    }

    public async Task AddAsync(T entity, CancellationToken ct = default)
    {
        await _dbSet.AddAsync(entity, ct);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}
