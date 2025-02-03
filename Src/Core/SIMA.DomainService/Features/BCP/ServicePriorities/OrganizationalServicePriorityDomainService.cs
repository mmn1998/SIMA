using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.ServicePriorities.Contracts;
using SIMA.Domain.Models.Features.BCP.ServicePriorities.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BCP.ServicePriorities;

public class OrganizationalServicePriorityDomainService : IOrganizationalServicePriorityDomainService
{
    private readonly SIMADBContext _context;

    public OrganizationalServicePriorityDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, OrganizationalServicePriorityId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.OrganizationalServicePriorities.AnyAsync(x => x.Code == code);
        else result = !await _context.OrganizationalServicePriorities.AnyAsync(x => x.Code == code && x.Id != Id && x.ActiveStatusId != 3);
        return result;
    }
}
