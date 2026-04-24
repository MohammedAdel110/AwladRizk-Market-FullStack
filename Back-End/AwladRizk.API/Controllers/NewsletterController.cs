using MediatR;
using Microsoft.AspNetCore.Mvc;
using AwladRizk.Application.Features.Newsletter.Commands;

namespace AwladRizk.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsletterController(ISender sender) : ControllerBase
{
    [HttpPost("subscribe")]
    public async Task<IActionResult> Subscribe([FromBody] NewsletterSubscribeRequest request, CancellationToken ct = default)
    {
        var success = await sender.Send(new SubscribeNewsletterCommand(request.Email), ct);
        return Ok(new { success });
    }
}

public record NewsletterSubscribeRequest(string Email);
