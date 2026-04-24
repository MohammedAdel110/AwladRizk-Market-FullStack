using AwladRizk.Domain.Entities;

namespace AwladRizk.Domain.Interfaces;

public interface IAdminUserRepository : IRepository<AdminUser>
{
    Task<AdminUser?> GetByEmailAsync(string email, CancellationToken ct = default);
}
