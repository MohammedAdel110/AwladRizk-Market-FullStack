using MediatR;
using Microsoft.AspNetCore.Mvc;
using AwladRizk.Application.Features.Payments.Commands;
using AwladRizk.Domain.Enums;

namespace AwladRizk.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController(ISender sender) : ControllerBase
{
    [HttpPost("process")]
    public async Task<IActionResult> Process([FromBody] ProcessPaymentRequest request, CancellationToken ct = default)
    {
        if (!Enum.TryParse<PaymentMethod>(request.PaymentMethod, true, out var paymentMethod))
        {
            return BadRequest(new { message = "Invalid payment method." });
        }

        var result = await sender.Send(new ProcessPaymentCommand(
            request.OrderNumber,
            paymentMethod,
            request.CardToken,
            request.WalletPhone,
            request.WalletProvider), ct);

        return Ok(result);
    }
}

public sealed class ProcessPaymentRequest
{
    public string OrderNumber { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = "Visa";
    public string? CardToken { get; set; }
    public string? WalletPhone { get; set; }
    public string? WalletProvider { get; set; }
}
