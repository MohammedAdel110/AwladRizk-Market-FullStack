using AwladRizk.Domain.Entities;

namespace AwladRizk.Domain.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<IReadOnlyList<Category>> GetAllWithProductsAsync(CancellationToken ct = default);
}
