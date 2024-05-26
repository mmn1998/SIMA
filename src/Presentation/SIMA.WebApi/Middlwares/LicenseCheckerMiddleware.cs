using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Options;
using Portable.Licensing;
using Portable.Licensing.Validation;
using SIMA.WebApi.Settings;

namespace SIMA.WebApi.Middlwares
{
    public class LicenseCheckerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;
        private readonly ApplicationSettings _options;

        public LicenseCheckerMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger, IOptions<ApplicationSettings> options)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._options = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint is null)
            {
                return;
            }
            var controllerActionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            if (controllerActionDescriptor is null)
            {
                return;
            }
            string controllerName = controllerActionDescriptor.ControllerName;
            string actionName = controllerActionDescriptor.ActionName;
            if (!File.Exists(_options.LicenseSettings.LicensePath))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Access is forbidden");
                return;
            }
            FileStream fileStream = new FileStream(_options.LicenseSettings.LicensePath, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                var license = License.Load(reader);
                var validationFailures = license.Validate()
                                .ExpirationDate()
                                .When(lic => lic.Type == LicenseType.Standard)
                                .And()
                                .Signature(_options.LicenseSettings.LicensePublicKey)
                                .AssertValidLicense()
                                .ToList();
                if (validationFailures.Any())
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Access is forbidden");
                    return;
                }

                var productFeatures = license.ProductFeatures.Get(controllerName.ToLower());
                string domainName = context.Request.Host.Host;
                var accessedDomain = license.ProductFeatures.Get("Domain");
                if (!accessedDomain.Equals(domainName, StringComparison.OrdinalIgnoreCase))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Access is forbidden");
                    return;
                }
                if (productFeatures is not null)
                {
                    bool.TryParse(productFeatures, out var hasAccess);
                    if (!hasAccess)
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.Response.WriteAsync("Access is forbidden");
                        return;
                    }
                }
            }
        }
    }
}
