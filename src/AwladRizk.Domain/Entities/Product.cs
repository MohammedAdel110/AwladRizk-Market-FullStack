using System.ComponentModel.DataAnnotations;
using AwladRizk.Domain.Common;

namespace AwladRizk.Domain.Entities;

public class Product : BaseEntity
{
    public string NameAr { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? OldPrice { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsOnSale { get; set; }
    public int StockQty { get; set; }

    /// <summary>
    /// Optimistic concurrency token.
    /// EF Core will check this on every UPDATE to prevent race conditions on stock.
    /// </summary>
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;

    // Foreign Key
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    // Navigation
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    /// <summary>
    /// Attempts to deduct stock. Returns false if insufficient.
    /// </summary>
    public bool TryDeductStock(int quantity)
    {
        if (StockQty < quantity) return false;
        StockQty -= quantity;
        UpdatedAt = DateTime.UtcNow;
        return true;
    }

    /// <summary>
    /// Restores stock (e.g., on order cancellation).
    /// </summary>
    public void RestoreStock(int quantity)
    {
        StockQty += quantity;
        UpdatedAt = DateTime.UtcNow;
    }
}
