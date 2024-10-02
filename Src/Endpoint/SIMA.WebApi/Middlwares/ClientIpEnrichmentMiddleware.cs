using Serilog.Context;

public class ClientIpEnrichmentMiddleware
{
    private readonly RequestDelegate _next;

    public ClientIpEnrichmentMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Get the client IP address from the HTTP context
        var clientIp = context.Connection.RemoteIpAddress?.ToString();

        // Add the client IP to the Serilog LogContext
        using (LogContext.PushProperty("ClientIP", clientIp))
        {
            await _next(context);
        }
    }
}
// Extension method used to add the middleware to the HTTP request pipeline
public static class ClientIpEnrichmentMiddlewareExtensions
{
    public static IApplicationBuilder UseClientIpEnrichmentMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ClientIpEnrichmentMiddleware>();
    }
}