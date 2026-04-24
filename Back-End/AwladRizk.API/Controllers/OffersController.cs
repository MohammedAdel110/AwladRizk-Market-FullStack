using MediatR;
using Microsoft.AspNetCore.Mvc;
using AwladRizk.Application.Features.Offers.Queries;

namespace AwladRizk.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OffersController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetActive(CancellationToken ct = default)
    {
        var result = await sender.Send(new GetActiveOffersQuery(), ct);
        return Ok(result);
    }
}
