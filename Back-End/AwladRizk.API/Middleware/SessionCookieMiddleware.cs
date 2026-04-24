namespace AwladRizk.API.Middleware;

public sealed class SessionCookieMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Session.Keys.Contains("session:init"))
        {
            context.Session.SetString("session:init", DateTime.UtcNow.ToString("O"));
        }

        await next(context);
    }
}
