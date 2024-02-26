using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sima.Framework.Core.Repository;
using SIMA.Framework.Core.Repository;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;
using SIMA.Persistance.Repositories.Features.Auths;

namespace SIMA.Persistance.ConfigurationExtentions;

public static class ConfigurationExtention
{
    public static IServiceCollection RegisterWriteDbContext(this IServiceCollection services, string connectionString)
    {
        return services.AddDbContext<DbContext, SIMADBContext>(options =>
            options.UseSqlServer(
                connectionString
                ));
    }
    public static IServiceCollection RegisterCommandRepository(this IServiceCollection services)
    {
        return services.Scan(scan =>
               scan.FromAssemblyOf<CompanyRepository>()
               .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
               .AsImplementedInterfaces()
               .WithScopedLifetime());
    }
    public static IServiceCollection RegisterUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
