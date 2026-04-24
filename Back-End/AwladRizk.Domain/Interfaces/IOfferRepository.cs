using AwladRizk.Domain.Entities;

namespace AwladRizk.Domain.Interfaces;

public interface IOfferRepository : IRepository<Offer>
{
    Task<IReadOnlyList<Offer>> GetActiveAsync(CancellationToken ct = default);
}
