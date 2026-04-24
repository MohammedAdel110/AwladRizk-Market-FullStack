using MediatR;
using Microsoft.AspNetCore.Mvc;
using AwladRizk.Application.Features.Orders.Commands;
using AwladRizk.Application.Features.Orders.Queries;
using AwladRizk.Domain.Enums;
using AwladRizk.Domain.Interfaces;

namespace AwladRizk.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(ISender sender, ICartService cartService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderRequest request, CancellationToken ct = default)
    {
        if (!Enum.TryParse<PaymentMethod>(request.PaymentMethod, true, out var paymentMethod))
        {
            return BadRequest(new { message = "Invalid payment method." });
        }

        var sessionId = string.IsNullOrWhiteSpace(request.SessionId) ? HttpContext.Session.Id : request.SessionId;

        // Allow web clients to submit cart line items directly at checkout,
        // then hydrate Redis cart before running the existing order pipeline.
        if (request.Items is { Count: > 0 })
        {
            await cartService.ClearAsync(sessionId, ct);
            foreach (var item in request.Items.Where(i => i.ProductId > 0 && i.Quantity > 0))
            {
                await cartService.AddItemAsync(sessionId, item.ProductId, item.Quantity, ct);
            }
        }

        var result = await sender.Send(new PlaceOrderCommand(
            sessionId,
            request.Street,
            request.Area ?? string.Empty,
            request.City,
            request.Governorate,
            request.Phone,
            request.Notes,
            paymentMethod,
            request.CardToken,
            request.WalletPhone,
            request.WalletProvider), ct);

        return Ok(result);
    }

    [HttpGet("{orderNumber}")]
    public async Task<IActionResult> GetByOrderNumber(string orderNumber, [FromQuery] string? sessionId, CancellationToken ct = default)
    {
        var effectiveSessionId = string.IsNullOrWhiteSpace(sessionId) ? HttpContext.Session.Id : sessionId;
        var order = await sender.Send(new GetOrderByNumberQuery(orderNumber, effectiveSessionId), ct);
        return order is null ? NotFound() : Ok(order);
    }

}

public sealed class PlaceOrderRequest
{
    public string? SessionId { get; set; }
    public string Governorate { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string? Area { get; set; }
    public string Street { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public string PaymentMethod { get; set; } = "Visa";
    public string? CardToken { get; set; }
    public string? WalletPhone { get; set; }
    public string? WalletProvider { get; set; }
    public List<PlaceOrderItemRequest>? Items { get; set; }
}

public sealed class PlaceOrderItemRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
