using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.OrganizationalProjects.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.OrganizationalProjects.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.ServiceCatalog.OrganizationalProjects;

public class OrganizationalProjectDomainService : IOrganizationalProjectDomainService
{
    private readonly SIMADBContext _context;

    public OrganizationalProjectDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, OrganizationalProjectId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.OrganizationalProjects.AnyAsync(x => x.Code == code);
        else result = !await _context.OrganizationalProjects.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}