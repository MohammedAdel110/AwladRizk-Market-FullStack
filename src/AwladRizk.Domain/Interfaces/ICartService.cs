namespace AwladRizk.Domain.Interfaces;

/// <summary>
/// Cart service interface — implemented by Redis in Infrastructure layer.
/// Cart is NOT stored in SQL; it lives in distributed cache.
/// </summary>
public interface ICartService
{
    Task<CartData> GetCartAsync(string sessionId, CancellationToken ct = default);
    Task AddItemAsync(string sessionId, int productId, int quantity = 1, CancellationToken ct = default);
    Task UpdateQuantityAsync(string sessionId, int productId, int quantity, CancellationToken ct = default);
    Task RemoveItemAsync(string sessionId, int productId, CancellationToken ct = default);
    Task ClearAsync(string sessionId, CancellationToken ct = default);
}

/// <summary>
/// Raw cart data stored in Redis.
/// </summary>
public class CartData
{
    public List<CartItemData> Items { get; set; } = new();
}

public class CartItemData
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public CartItemData() { }
    public CartItemData(int productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }
}
