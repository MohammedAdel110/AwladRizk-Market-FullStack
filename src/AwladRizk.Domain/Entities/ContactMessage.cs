using AwladRizk.Domain.Common;

namespace AwladRizk.Domain.Entities;

public class ContactMessage : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
}
