using System.Security.Claims;
using System.Text;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Invoke(HttpContext context)
    {


        if (context.Request.Method == "POST" || context.Request.Method == "PUT" || context.Request.Method == "DELETE")
        {
            // Log additional information as needed
            var requestBody = await FormatRequest(context.Request);
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _logger.LogInformation($"Request {context.Request.Method}: {context.Request.Path}, Body: {requestBody},UserId: {userId}");
        }
        // Call the next delegate/middleware in the pipeline
        await _next(context);

        // Log response details for specific HTTP methods
        if (context.Request.Method == HttpMethods.Put ||
            context.Request.Method == HttpMethods.Post ||
            context.Request.Method == HttpMethods.Delete)
        {
            LogResponse(context);
        }
    }
    private async Task<string> FormatRequest(HttpRequest request)
    {
        request.EnableBuffering();

        using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
        {
            var body = await reader.ReadToEndAsync();
            request.Body.Seek(0, SeekOrigin.Begin);
            return body;
        }
    }
    private void LogResponse(HttpContext context)
    {
        var response = context.Response;
        var responseBody = string.Empty;

        if (response.Body.CanSeek) { 
        // Capture response body
        using (var streamReader = new StreamReader(response.Body, Encoding.UTF8, true, 1024, true))
        {
            responseBody = streamReader.ReadToEnd();
            response.Body.Position = 0; // Reset the stream position to zero so that the response can be read by other components
        }
    }
        _logger.LogInformation($"Response {context.Request.Method}: {context.Request.Path}, Status Code: {response.StatusCode}, Body: {responseBody}");
    }
}

// Extension method used to add the middleware to the HTTP request pipeline
public static class RequestResponseLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
    }
}