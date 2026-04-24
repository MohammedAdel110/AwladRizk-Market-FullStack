using Microsoft.EntityFrameworkCore;
using AwladRizk.Application.DTOs;
using AwladRizk.Domain.Interfaces;
using AwladRizk.Persistence;

namespace AwladRizk.Infrastructure.Cart;

/// <summary>
/// Enriches raw CartData (from Redis) with product details (from SQL).
/// This is the bridge between the cache and the API response.
/// </summary>
public class CartEnrichmentService
{
    private readonly AwladRizkDbContext _db;

    /// <summary>
    /// Delivery fee in EGP — matches frontend cart_delivery_fee: "15 EGP".
    /// </summary>
    private const decimal DeliveryFee = 15m;

    /// <summary>
    /// Minimum order for free delivery — matches frontend FreeDeliveryMin: 200.
    /// </summary>
    private const decimal FreeDeliveryMinimum = 200m;

    public CartEnrichmentService(AwladRizkDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Converts raw CartData into a fully enriched CartDto
    /// with product names, images, prices, and calculated totals.
    /// </summary>
    public async Task<CartDto> EnrichAsync(CartData cartData, CancellationToken ct = default)
    {
        if (cartData.Items.Count == 0)
        {
            return new CartDto
            {
                Items = new List<CartItemDto>(),
                SubTotal = 0,
                DeliveryFee = 0,
                GrandTotal = 0,
                FreeDeliveryMin = FreeDeliveryMinimum
            };
        }

        var productIds = cartData.Items.Select(i => i.ProductId).ToList();

        var products = await _db.Products
            .AsNoTracking()
            .Where(p => productIds.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id, ct);

        var items = new List<CartItemDto>();

        foreach (var cartItem in cartData.Items)
        {
            if (!products.TryGetValue(cartItem.ProductId, out var product))
                continue; // skip items whose product was deleted

            items.Add(new CartItemDto
            {
                ProductId = product.Id,
                NameAr = product.NameAr,
                NameEn = product.NameEn,
                ImageUrl = product.ImageUrl,
                UnitPrice = product.Price,
                Quantity = cartItem.Quantity
            });
        }

        var subTotal = items.Sum(i => i.UnitPrice * i.Quantity);
        var deliveryFee = subTotal >= FreeDeliveryMinimum ? 0m : DeliveryFee;

        return new CartDto
        {
            Items = items,
            SubTotal = subTotal,
            DeliveryFee = deliveryFee,
            GrandTotal = subTotal + deliveryFee,
            FreeDeliveryMin = FreeDeliveryMinimum
        };
    }
}
