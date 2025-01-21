using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BCP.PlanResponsibilities;

public class PlanResponsibilityDomainService : IPlanResponsibilityDomainService
{
    private readonly SIMADBContext _context;

    public PlanResponsibilityDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, PlanResponsibilityId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.PlanResponsibilities.AnyAsync(x => x.Code == code);
        else result = !await _context.PlanResponsibilities.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}