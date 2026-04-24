using Microsoft.AspNetCore.Mvc;

namespace AwladRizk.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SystemController : ControllerBase
{
    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(new
        {
            status = "Healthy",
            serverTimeUtc = DateTime.UtcNow
        });
    }

    [HttpGet("session")]
    public IActionResult SessionInfo()
    {
        var initializedAt = HttpContext.Session.GetString("session:init");

        return Ok(new
        {
            sessionId = HttpContext.Session.Id,
            initializedAtUtc = initializedAt
        });
    }
}
