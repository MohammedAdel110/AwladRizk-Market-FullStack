using MediatR;
using Microsoft.AspNetCore.Mvc;
using AwladRizk.Application.Features.Contact.Commands;

namespace AwladRizk.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Send([FromBody] ContactRequest request, CancellationToken ct = default)
    {
        var success = await sender.Send(
            new SendContactMessageCommand(request.Name, request.Email, request.Phone, request.Message),
            ct);

        return Ok(new { success });
    }
}

public record ContactRequest(string Name, string Email, string? Phone, string Message);
