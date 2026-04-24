using MediatR;
using Microsoft.AspNetCore.Mvc;
using AwladRizk.Application.Features.Auth.Commands;

namespace AwladRizk.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("admin-login")]
    public async Task<IActionResult> AdminLogin([FromBody] AdminLoginRequest request, CancellationToken ct = default)
    {
        var result = await sender.Send(new AdminLoginCommand(request.Email, request.Password), ct);
        return result is null ? Unauthorized(new { message = "Invalid credentials." }) : Ok(result);
    }
}

public record AdminLoginRequest(string Email, string Password);
