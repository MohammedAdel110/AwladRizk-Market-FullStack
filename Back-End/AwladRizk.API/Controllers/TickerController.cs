using Microsoft.AspNetCore.Mvc;
using AwladRizk.Domain.Interfaces;

namespace AwladRizk.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TickerController(IHomeTickerRepository homeTickerRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetActive(CancellationToken ct = default)
    {
        var items = await homeTickerRepository.GetActiveOrderedAsync(ct);
        var response = items.Select(x => new
        {
            x.Id,
            x.TextAr,
            x.TextEn,
            x.SortOrder
        });
        return Ok(response);
    }
}
