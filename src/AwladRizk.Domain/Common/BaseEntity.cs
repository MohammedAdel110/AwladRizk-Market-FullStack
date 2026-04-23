namespace AwladRizk.Domain.Common;

/// <summary>
/// Base entity with shared audit fields.
/// All domain entities inherit from this.
/// </summary>
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
