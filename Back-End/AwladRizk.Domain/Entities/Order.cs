using AwladRizk.Domain.Common;
using AwladRizk.Domain.Enums;
using AwladRizk.Domain.ValueObjects;

namespace AwladRizk.Domain.Entities;

public class Order : BaseEntity
{
    public string OrderNumber { get; set; } = string.Empty;
    public string SessionId { get; set; } = string.Empty;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public string CustomerName { get; set; } = string.Empty;

    // Totals
    public decimal SubTotal { get; set; }
    public decimal DeliveryFee { get; set; }
    public decimal GrandTotal { get; set; }

    // Delivery info
    public string DeliveryStreet { get; set; } = string.Empty;
    public string DeliveryArea { get; set; } = string.Empty;
    public string DeliveryCity { get; set; } = string.Empty;
    public string DeliveryGovernorate { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Notes { get; set; }

    // Navigation
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    public Payment? Payment { get; set; }

    /// <summary>
    /// Gets the delivery address as a value object.
    /// </summary>
    public Address GetDeliveryAddress() =>
        new(DeliveryStreet, DeliveryArea, DeliveryCity, DeliveryGovernorate);

    /// <summary>
    /// Sets the delivery address from a value object.
    /// </summary>
    public void SetDeliveryAddress(Address address)
    {
        DeliveryStreet = address.Street;
        DeliveryArea = address.Area;
        DeliveryCity = address.City;
        DeliveryGovernorate = address.Governorate;
    }

    /// <summary>
    /// Generates a unique order number.
    /// Format: ARK-YYYYMMDD-XXXX
    /// </summary>
    public static string GenerateOrderNumber()
    {
        var date = DateTime.UtcNow.ToString("yyyyMMdd");
        var random = Random.Shared.Next(1000, 9999);
        return $"ARK-{date}-{random}";
    }
}
