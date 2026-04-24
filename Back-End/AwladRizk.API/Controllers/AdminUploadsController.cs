using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AwladRizk.API.Controllers;

[ApiController]
[Route("api/admin/uploads")]
[Authorize(Roles = "Admin")]
public sealed class AdminUploadsController(IWebHostEnvironment env) : ControllerBase
{
    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".jpeg", ".png", ".webp", ".gif"
    };

    [HttpPost("image")]
    [RequestSizeLimit(5_000_000)] // 5MB
    public async Task<IActionResult> UploadImage([FromForm] IFormFile file, CancellationToken ct)
    {
        if (file is null || file.Length <= 0)
        {
            return BadRequest(new { title = "File is required." });
        }

        var ext = Path.GetExtension(file.FileName ?? string.Empty);
        if (string.IsNullOrWhiteSpace(ext) || !AllowedExtensions.Contains(ext))
        {
            return BadRequest(new { title = "Unsupported image type." });
        }

        var webRoot = env.WebRootPath;
        if (string.IsNullOrWhiteSpace(webRoot))
        {
            webRoot = Path.Combine(env.ContentRootPath, "wwwroot");
        }

        var uploadDir = Path.Combine(webRoot, "uploads");
        Directory.CreateDirectory(uploadDir);

        var safeName = $"{DateTime.UtcNow:yyyyMMddHHmmssfff}_{Guid.NewGuid():N}{ext.ToLowerInvariant()}";
        var fullPath = Path.Combine(uploadDir, safeName);

        await using (var stream = System.IO.File.Create(fullPath))
        {
            await file.CopyToAsync(stream, ct);
        }

        // Served by app.UseStaticFiles() as /uploads/{file}
        var relativeUrl = $"/uploads/{safeName}";
        var absoluteUrl = $"{Request.Scheme}://{Request.Host}{relativeUrl}";
        return Ok(new { url = absoluteUrl, relativeUrl });
    }
}

