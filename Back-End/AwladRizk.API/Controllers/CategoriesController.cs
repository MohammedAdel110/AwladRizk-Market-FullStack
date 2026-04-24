using MediatR;
using Microsoft.AspNetCore.Mvc;
using AwladRizk.Application.Features.Categories.Queries;

namespace AwladRizk.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct = default)
    {
        var result = await sender.Send(new GetAllCategoriesQuery(), ct);
        return Ok(result);
    }
}
