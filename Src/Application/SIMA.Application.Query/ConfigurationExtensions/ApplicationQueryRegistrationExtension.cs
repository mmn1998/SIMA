using Microsoft.Extensions.DependencyInjection;
using SIMA.Application.Query.Features.Auths.AddressTypes.Mappers;
using SIMA.Application.Query.Features.Auths.Companies;
using SIMA.Application.Query.Features.Auths.Companies.Mappers;
using SIMA.Application.Query.Features.Auths.ConfigurationAttributes.Mappers;
using SIMA.Application.Query.Features.Auths.Departments.Mappers;
using SIMA.Application.Query.Features.Auths.Domains.Mappers;
using SIMA.Application.Query.Features.Auths.SysConfigs.Mappers;
using SIMA.Application.Query.Features.Auths.Users.Mappers;
using SIMA.Application.Query.Services.SimaReposrtServices;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.ConfigurationExtensions;

public static class ApplicationQueryRegistrationExtension
{
    public static IServiceCollection RegisterQueryHandlerService(this IServiceCollection services)
    {
        services.AddScoped<ISimaReposrtService, SimaReposrtService>();
        return services.Scan(scan =>
                scan.FromAssemblyOf<CompanyQueryHandler>()
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
    }

    public static IServiceCollection RegisterQueryMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(conf =>
        {
            conf.AddProfile(typeof(AddressTypeQueryMapper));
            conf.AddProfile(typeof(ConfigurationAttributeQueryMapper));
            conf.AddProfile(typeof(DepartmentQueryMapper));
            conf.AddProfile(typeof(SysConfigQueryMapper));
            conf.AddProfile(typeof(UserQueryMapper));
            conf.AddProfile(typeof(CompanyQueryMapper));
            conf.AddProfile(typeof(DomainQueryMapper));

        });
        return services;
    }
}
