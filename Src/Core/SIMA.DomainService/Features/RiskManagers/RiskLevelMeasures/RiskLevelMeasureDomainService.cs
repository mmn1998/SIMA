using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelMeasures.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.RiskLevelMeasures;

public class RiskLevelMeasureDomainService : IRiskLevelMeasureDomainService
{
    private readonly SIMADBContext _context;

    public RiskLevelMeasureDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, long id)
    {
        if (id > 0)
            return !await _context.RiskLevelMeasures.AnyAsync(b => b.Code == code && b.Id != new RiskLevelMeasureId(id));
        else
            return !await _context.RiskLevelMeasures.AnyAsync(b => b.Code == code);
    }

    public async Task<bool> IsUnique(RiskPossibilityId riskPossibilityId, RiskLevelId riskLevelId, RiskImpactId riskImpactId, RiskLevelMeasureId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.RiskLevelMeasures.AnyAsync(x => x.RiskPossibilityId == riskPossibilityId && x.RiskLevelId == riskLevelId && x.RiskImpactId == riskImpactId);
        else result = !await _context.RiskLevelMeasures.AnyAsync(x => x.RiskPossibilityId == riskPossibilityId && x.RiskLevelId == riskLevelId && x.RiskImpactId == riskImpactId && x.Id != id);
        return result;
    }
}
