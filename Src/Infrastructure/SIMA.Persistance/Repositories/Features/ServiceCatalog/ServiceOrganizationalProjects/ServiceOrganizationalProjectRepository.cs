using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceOrganizationalProjects.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceOrganizationalProjects.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.ServiceCatalog.ServiceOrganizationalProjects;

public class ServiceOrganizationalProjectRepository : Repository<ServiceOrganizationalProject>, IServiceOrganizationalProjectRepository
{
    private readonly SIMADBContext _context;

    public ServiceOrganizationalProjectRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ServiceOrganizationalProject> GetById(ServiceOrganizationalProjectId id)
    {
        var entity = await _context.ServiceOrganizationalProjects
                .Include(x => x.Service)
                .FirstOrDefaultAsync(x => x.Id == id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}