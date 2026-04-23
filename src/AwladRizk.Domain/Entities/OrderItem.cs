using AwladRizk.Domain.Common;

namespace AwladRizk.Domain.Entities;

/// <summary>
/// Snapshot of product at order time.
/// Price and name are captured so they don't change retroactively.
/// </summary>
public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    /// <summary>
    /// Product name captured at order time (immutable snapshot).
    /// </summary>
    public string ProductNameSnapshot { get; set; } = string.Empty;

    public int Quantity { get; set; }

    /// <summary>
    /// Unit price captured at order time (immutable snapshot).
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Calculated line total.
    /// </summary>
    public decimal LineTotal => UnitPrice * Quantity;
}
