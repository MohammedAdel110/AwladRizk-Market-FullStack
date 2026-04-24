using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AwladRizk.Application.Features.Admin.Commands;
using AwladRizk.Application.Features.Admin.Queries;

namespace AwladRizk.API.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public class AdminManagementController(ISender sender) : ControllerBase
{
    [HttpPost("products")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command, CancellationToken ct = default)
    {
        var result = await sender.Send(command, ct);
        return Ok(result);
    }

    [HttpPut("products/{id:int}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductBody body, CancellationToken ct = default)
    {
        var result = await sender.Send(new UpdateProductCommand(
            id,
            body.NameAr,
            body.NameEn,
            body.Price,
            body.OldPrice,
            body.ImageUrl,
            body.IsOnSale,
            body.StockQty,
            body.CategoryId), ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("products/{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id, CancellationToken ct = default)
    {
        var deleted = await sender.Send(new DeleteProductCommand(id), ct);
        return deleted ? NoContent() : NotFound();
    }

    [HttpPost("offers")]
    public async Task<IActionResult> CreateOffer([FromBody] CreateOfferCommand command, CancellationToken ct = default)
    {
        var result = await sender.Send(command, ct);
        return Ok(result);
    }

    [HttpPut("offers/{id:int}")]
    public async Task<IActionResult> UpdateOffer(int id, [FromBody] UpdateOfferBody body, CancellationToken ct = default)
    {
        var result = await sender.Send(new UpdateOfferCommand(
            id,
            body.TitleAr,
            body.TitleEn,
            body.DescriptionAr,
            body.DescriptionEn,
            body.DiscountPercent,
            body.BadgeText,
            body.Icon,
            body.ValidUntil,
            body.IsActive), ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("offers/{id:int}")]
    public async Task<IActionResult> DeleteOffer(int id, CancellationToken ct = default)
    {
        var deleted = await sender.Send(new DeleteOfferCommand(id), ct);
        return deleted ? NoContent() : NotFound();
    }

    [HttpGet("ticker-messages")]
    public async Task<IActionResult> GetTickerMessages(CancellationToken ct = default)
    {
        var result = await sender.Send(new GetAllTickerMessagesQuery(), ct);
        return Ok(result);
    }

    [HttpPost("ticker-messages")]
    public async Task<IActionResult> CreateTicker([FromBody] CreateTickerMessageCommand command, CancellationToken ct = default)
    {
        var result = await sender.Send(command, ct);
        return Ok(result);
    }

    [HttpPut("ticker-messages/{id:int}")]
    public async Task<IActionResult> UpdateTicker(int id, [FromBody] UpdateTickerBody body, CancellationToken ct = default)
    {
        var result = await sender.Send(new UpdateTickerMessageCommand(id, body.TextAr, body.TextEn, body.SortOrder, body.IsActive), ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("ticker-messages/{id:int}")]
    public async Task<IActionResult> DeleteTicker(int id, CancellationToken ct = default)
    {
        var deleted = await sender.Send(new DeleteTickerMessageCommand(id), ct);
        return deleted ? NoContent() : NotFound();
    }
}

public sealed class UpdateProductBody
{
    public string NameAr { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? OldPrice { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsOnSale { get; set; }
    public int StockQty { get; set; }
    public int CategoryId { get; set; }
}

public sealed class UpdateOfferBody
{
    public string TitleAr { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;
    public string DescriptionAr { get; set; } = string.Empty;
    public string DescriptionEn { get; set; } = string.Empty;
    public int DiscountPercent { get; set; }
    public string BadgeText { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public DateTime ValidUntil { get; set; }
    public bool IsActive { get; set; }
}

public sealed class UpdateTickerBody
{
    public string TextAr { get; set; } = string.Empty;
    public string TextEn { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}
