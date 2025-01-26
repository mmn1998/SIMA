using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.AffectedHistories;

public class AffectedHistoryDomainService : IAffectedHistoryDomainService
{
    private readonly SIMADBContext _context;

    public AffectedHistoryDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, AffectedHistoryId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.AffectedHistories.AnyAsync(x => x.Code == code);
        else result = !await _context.AffectedHistories.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }

    public async Task<bool> IsNumericUnique(float value, AffectedHistoryId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.AffectedHistories.AnyAsync(x => x.NumericValue == value);
        else result = !await _context.AffectedHistories.AnyAsync(x => x.NumericValue == value && x.Id != id);
        return result;
    }
}
