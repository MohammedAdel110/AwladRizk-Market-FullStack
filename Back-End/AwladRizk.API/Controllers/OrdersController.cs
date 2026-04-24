using MediatR;
using Microsoft.AspNetCore.Mvc;
using AwladRizk.Application.Features.Orders.Commands;
using AwladRizk.Application.Features.Orders.Queries;
using AwladRizk.Domain.Enums;

namespace AwladRizk.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderRequest request, CancellationToken ct = default)
    {
        if (!Enum.TryParse<PaymentMethod>(request.PaymentMethod, true, out var paymentMethod))
        {
            return BadRequest(new { message = "Invalid payment method." });
        }

        var (street, area, city, governorate) = SplitAddress(request.Address);
        var sessionId = string.IsNullOrWhiteSpace(request.SessionId) ? HttpContext.Session.Id : request.SessionId;

        var result = await sender.Send(new PlaceOrderCommand(
            sessionId,
            street,
            area,
            city,
            governorate,
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

    private static (string street, string area, string city, string governorate) SplitAddress(string? rawAddress)
    {
        if (string.IsNullOrWhiteSpace(rawAddress))
        {
            return ("N/A", "N/A", "N/A", "N/A");
        }

        var parts = rawAddress
            .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ToList();

        while (parts.Count < 4)
        {
            parts.Add("N/A");
        }

        return (parts[0], parts[1], parts[2], parts[3]);
    }
}

public sealed class PlaceOrderRequest
{
    public string? SessionId { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public string PaymentMethod { get; set; } = "Visa";
    public string? CardToken { get; set; }
    public string? WalletPhone { get; set; }
    public string? WalletProvider { get; set; }
}
