using MediatR;
using Microsoft.AspNetCore.Mvc;
using AwladRizk.Application.Features.Products.Queries;

namespace AwladRizk.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? categorySlug,
        [FromQuery] string? search,
        [FromQuery] bool? onSaleOnly,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 12,
        CancellationToken ct = default)
    {
        var result = await sender.Send(new GetAllProductsQuery(categorySlug, search, onSaleOnly, page, pageSize), ct);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct = default)
    {
        var result = await sender.Send(new GetProductByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("featured")]
    public async Task<IActionResult> GetFeatured([FromQuery] int count = 6, CancellationToken ct = default)
    {
        var result = await sender.Send(new GetFeaturedProductsQuery(count), ct);
        return Ok(result);
    }
}
