using AwladRizk.Domain.Entities;

namespace AwladRizk.Domain.Interfaces;

public interface IHomeTickerRepository : IRepository<HomeTickerMessage>
{
    Task<IReadOnlyList<HomeTickerMessage>> GetAllOrderedAsync(CancellationToken ct = default);
    Task<IReadOnlyList<HomeTickerMessage>> GetActiveOrderedAsync(CancellationToken ct = default);
}
