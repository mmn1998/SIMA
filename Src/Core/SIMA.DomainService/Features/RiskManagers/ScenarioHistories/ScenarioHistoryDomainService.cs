using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.ScenarioHistories.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.ScenarioHistories;

public class ScenarioHistoryDomainService : IScenarioHistoryDomainService
{
    private readonly SIMADBContext _context;

    public ScenarioHistoryDomainService(SIMADBContext context)
    {
        _context = context;
    }

    public async Task<bool> IsCodeUnique(string code, ScenarioHistoryId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.ScenarioHistories.AnyAsync(x => x.Code == code);
        else result = !await _context.ScenarioHistories.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }

    public async Task<bool> IsNumericUnique(float value, ScenarioHistoryId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.ScenarioHistories.AnyAsync(x => x.NumericValue == value);
        else result = !await _context.ScenarioHistories.AnyAsync(x => x.NumericValue == value && x.Id != id);
        return result;
    }
}