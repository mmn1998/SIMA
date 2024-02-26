using Microsoft.Extensions.DependencyInjection;
using SIMA.Framework.Core.Repository;
using SIMA.Persistance.Read.Repositories.Features.Auths.Genders;
using SIMA.Persistance.Read.Repositories.Features.Auths.Companies;
using SIMA.Persistance.Read.Repositories.Features.Auths.LocationTypes;
using SIMA.Persistance.Read.Repositories.Features.Auths.AddressTypes;
using SIMA.Persistance.Read.Repositories.Features.Auths.Genders.Decorators;
using SIMA.Persistance.Read.Repositories.Features.Auths.Locations;
using SIMA.Persistance.Read.Repositories.Features.Auths.LocationTypes.LocationTypes;
using SIMA.Persistance.Read.Repositories.Features.Auths.Locations.Decorators;
using SIMA.Persistance.Read.Repositories.Features.Auths.AddressTypes.Decorators;

namespace SIMA.Persistance.Read.ConfigurationExtensions;
public static class ConfigurationExtensions
{
    public static IServiceCollection RegisterQueryRepositories(this IServiceCollection services)
    {
        services.Scan(scan =>
        {
            scan.FromAssemblyOf<CompanyQueryRepository>()
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryRepository)))
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });

        #region Decorators Registrations

        services.AddScoped<IGenderQueryRepository, GenderQueryRepository>();
        services.Decorate<IGenderQueryRepository, GenderQueryRepositoryCachingDecorator>();
        services.AddScoped<ILocationQueryRepository, LocationQueryRepository>();
        services.Decorate<ILocationQueryRepository, LocationQueryRepositoryCachingDecorator>();
        services.AddScoped<ILocationTypeQueryRepository, LocationTypeQueryRepository>();
        services.Decorate<ILocationTypeQueryRepository, LocationTypeQueryRepositoryCachingDecorator>();
        services.AddScoped<IAddressTypeQueryRepository, AddressTypeQueryRepository>();
        services.Decorate<IAddressTypeQueryRepository, AddressTypeQueryRepositoryCachingDecorator>();
        #endregion
        return services;
    }
}
