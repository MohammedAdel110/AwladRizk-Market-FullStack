using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using AwladRizk.Domain.Interfaces;
using AwladRizk.Persistence;

namespace AwladRizk.Infrastructure.Cart;

/// <summary>
/// Redis-backed cart service.
/// Cart items are stored as JSON in distributed cache keyed by SessionId.
/// Product data (names, images, prices) is enriched from SQL on read.
/// </summary>
public class RedisCartService : ICartService
{
    private readonly IDistributedCache _cache;
    private readonly AwladRizkDbContext _db;

    private static readonly DistributedCacheEntryOptions _cacheOptions = new()
    {
        SlidingExpiration = TimeSpan.FromDays(7)
    };

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public RedisCartService(IDistributedCache cache, AwladRizkDbContext db)
    {
        _cache = cache;
        _db = db;
    }

    private static string Key(string sessionId) => $"cart:{sessionId}";

    public async Task<CartData> GetCartAsync(string sessionId, CancellationToken ct = default)
    {
        var json = await _cache.GetStringAsync(Key(sessionId), ct);

        if (string.IsNullOrEmpty(json))
            return new CartData();

        return JsonSerializer.Deserialize<CartData>(json, _jsonOptions) ?? new CartData();
    }

    public async Task AddItemAsync(string sessionId, int productId, int quantity = 1, CancellationToken ct = default)
    {
        // Validate product exists
        var productExists = await _db.Products
            .AsNoTracking()
            .AnyAsync(p => p.Id == productId, ct);

        if (!productExists)
            throw new ArgumentException($"Product with ID {productId} not found.");

        var cart = await GetCartAsync(sessionId, ct);
        var existing = cart.Items.FirstOrDefault(i => i.ProductId == productId);

        if (existing is not null)
        {
            existing.Quantity += quantity;
        }
        else
        {
            cart.Items.Add(new CartItemData(productId, quantity));
        }

        await SaveCartAsync(sessionId, cart, ct);
    }

    public async Task UpdateQuantityAsync(string sessionId, int productId, int quantity, CancellationToken ct = default)
    {
        if (quantity <= 0)
        {
            await RemoveItemAsync(sessionId, productId, ct);
            return;
        }

        var cart = await GetCartAsync(sessionId, ct);
        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

        if (item is null)
            throw new ArgumentException($"Product {productId} not found in cart.");

        item.Quantity = quantity;
        await SaveCartAsync(sessionId, cart, ct);
    }

    public async Task RemoveItemAsync(string sessionId, int productId, CancellationToken ct = default)
    {
        var cart = await GetCartAsync(sessionId, ct);
        cart.Items.RemoveAll(i => i.ProductId == productId);
        await SaveCartAsync(sessionId, cart, ct);
    }

    public async Task ClearAsync(string sessionId, CancellationToken ct = default)
    {
        await _cache.RemoveAsync(Key(sessionId), ct);
    }

    private async Task SaveCartAsync(string sessionId, CartData cart, CancellationToken ct)
    {
        var json = JsonSerializer.Serialize(cart, _jsonOptions);
        await _cache.SetStringAsync(Key(sessionId), json, _cacheOptions, ct);
    }
}
