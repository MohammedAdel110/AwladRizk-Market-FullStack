using AwladRizk.Domain.Entities;

namespace AwladRizk.Domain.Interfaces;

/// <summary>
/// JWT token service interface.
/// </summary>
public interface IJwtTokenService
{
    string GenerateToken(AdminUser user);
}
