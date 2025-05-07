using Microsoft.Extensions.DependencyInjection;
using SIMA.DomainService.Features.Auths.ConfigurationAttributes;
using SIMA.Framework.Core.Domain;

namespace SIMA.DomainService.ConfigurationExtensions
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection RegisterDomainServices(this IServiceCollection services)
        {
            return services.Scan(scan =>
                   scan.FromAssemblyOf<ConfigurationAttributeService>()
                   .AddClasses(classes => classes.AssignableTo(typeof(IDomainService)))
                   .AsImplementedInterfaces()
                   .WithScopedLifetime());
        }
    }
}
