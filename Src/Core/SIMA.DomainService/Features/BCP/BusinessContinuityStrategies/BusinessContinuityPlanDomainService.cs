using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Contracts;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BCP.BusinessContinuityStrategies;

public class BusinessContinuityPlanDomainService : IBusinessContinuityPlanDomainService
{
    private readonly SIMADBContext _context;

    public BusinessContinuityPlanDomainService(SIMADBContext context)
    {
        _context = context;
    }

    public async Task<bool> IsCodeUnique(string code, BusinessContinuityPlanId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.BusinessContinuityPlans.AnyAsync(x => x.Code == code);
        else result = !await _context.BusinessContinuityPlans.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }

    public async Task<bool> IsVersionUnique(string versionNumber, BusinessContinuityPlanId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.BusinessContinuityPlans.AnyAsync(x => x.VersionNumber == versionNumber);
        else result = !await _context.BusinessContinuityPlans.AnyAsync(x => x.VersionNumber == versionNumber && x.Id != id);
        return result;
    }
}