using Microsoft.EntityFrameworkCore;
using AwladRizk.Domain.Entities;
using AwladRizk.Domain.Interfaces;

namespace AwladRizk.Persistence.Repositories;

public class AdminUserRepository : GenericRepository<AdminUser>, IAdminUserRepository
{
    public AdminUserRepository(AwladRizkDbContext context) : base(context) { }

    public async Task<AdminUser?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        return await _dbSet.FirstOrDefaultAsync(
            a => a.Email.ToLower() == email.ToLower(),
            ct);
    }
}
