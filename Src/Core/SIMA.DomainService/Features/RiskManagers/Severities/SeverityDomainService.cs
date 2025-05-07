using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.Severities.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.Severities.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.Severities;

public class SeverityDomainService : ISeverityDomainService
{
    private readonly SIMADBContext _context;

    public SeverityDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, SeverityId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.Severities.AnyAsync(x => x.Code == code);
        else result = !await _context.Severities.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }

    public async Task<bool> IsRelationsUnique(AffectedHistoryId AffectedHistoryId, SeverityValueId SeverityValueId,
        ConsequenceLevelId ConsequenceLevelId, SeverityId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.Severities.AnyAsync(x => x.AffectedHistoryId == AffectedHistoryId &&x.SeverityValueId == SeverityValueId&&x.ConsequenceLevelId == ConsequenceLevelId);
        else result = !await _context.Severities.AnyAsync(x => x.AffectedHistoryId == AffectedHistoryId &&x.SeverityValueId == SeverityValueId&&x.ConsequenceLevelId == ConsequenceLevelId && x.Id != id);
        return result;
    }
}