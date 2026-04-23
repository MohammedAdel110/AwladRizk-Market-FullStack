using AwladRizk.Domain.Common;

namespace AwladRizk.Domain.Interfaces;

/// <summary>
/// Generic repository for WRITE operations.
/// Read operations should use DbContext directly with AsNoTracking().
/// </summary>
public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id, CancellationToken ct = default);
    Task AddAsync(T entity, CancellationToken ct = default);
    void Update(T entity);
    void Delete(T entity);
}
