using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SIMA.WebApi.Middlwares;

public class DisAllowDirectDownloadMiddleware
{
    private readonly RequestDelegate _next;

    public DisAllowDirectDownloadMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        if (httpContext.Request.Path.HasValue)
        {
            if (httpContext.Request.Path.Value.ToLower().Contains("useruploadfiles"))
            {
                await httpContext.Response.WriteAsync("direct download is not allowed !");
            }
            else
                await _next(httpContext);
        }
        else
            await _next(httpContext);
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class DisAllowDirectDownloadMiddlewareExtensions
{
    public static IApplicationBuilder UseDisAllowDirectDownloadMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<DisAllowDirectDownloadMiddleware>();
    }
}
