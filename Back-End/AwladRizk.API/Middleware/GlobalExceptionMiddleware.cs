using System.Text.Json;

namespace AwladRizk.API.Middleware;

public sealed class GlobalExceptionMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionMiddleware> logger,
    IHostEnvironment environment)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception while processing {Path}", context.Request.Path);
            await WriteProblemDetailsAsync(context, ex, environment.IsDevelopment());
        }
    }

    private static async Task WriteProblemDetailsAsync(HttpContext context, Exception exception, bool includeDetails)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var response = new
        {
            title = "An unexpected error occurred.",
            status = StatusCodes.Status500InternalServerError,
            traceId = context.TraceIdentifier,
            detail = includeDetails ? exception.Message : "Please contact support if the problem persists."
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
