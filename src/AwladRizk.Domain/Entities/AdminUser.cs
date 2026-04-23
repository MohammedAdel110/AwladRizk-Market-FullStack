using AwladRizk.Domain.Common;

namespace AwladRizk.Domain.Entities;

public class AdminUser : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = "Admin";
}
