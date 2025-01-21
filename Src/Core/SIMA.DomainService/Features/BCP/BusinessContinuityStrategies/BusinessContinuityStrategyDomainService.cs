using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Contracts;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BCP.BusinessContinuityStrategies;

public class BusinessContinuityStrategyDomainService : IBusinessContinuityStategyDomainService
{
    private readonly SIMADBContext _context;

    public BusinessContinuityStrategyDomainService(SIMADBContext context)
    {
        _context = context;
    }

    public async Task<bool> IsCodeUnique(string code, BusinessContinuityStrategyId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.BusinessContinuityStrategies.AnyAsync(x => x.Code == code);
        else result = !await _context.BusinessContinuityStrategies.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }
    public async Task<bool> IsObjectiveCodeUnique(string code, BusinessContinuityStrategyObjectiveId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.BusinessContinuityStrategyObjectives.AnyAsync(x => x.Code == code);
        else result = !await _context.BusinessContinuityStrategyObjectives.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }
    public async Task<bool> IsSoloutionCodeUnique(string code, BusinessContinuityStratgySolutionId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.BusinessContinuityStratgySolutions.AnyAsync(x => x.Code == code);
        else result = !await _context.BusinessContinuityStratgySolutions.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }
}