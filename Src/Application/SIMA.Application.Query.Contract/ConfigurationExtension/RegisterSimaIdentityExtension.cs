using Microsoft.Extensions.DependencyInjection;
using SIMA.Framework.Common.Security;

namespace SIMA.Application.Query.Contract.ConfigurationExtension;

public static class RegisterSimaIdentityExtension
{
    public static IServiceCollection RegisterSimaIdentity(this IServiceCollection services)
    {
        return services.AddScoped<ISimaIdentity, SimaIdentity>();
    }
}
