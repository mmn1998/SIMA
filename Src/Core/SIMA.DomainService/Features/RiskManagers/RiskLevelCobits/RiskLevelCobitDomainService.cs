using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.RiskLevelCobits;

public class RiskLevelCobitDomainService : IRiskLevelCobitDomainService
{
    private readonly SIMADBContext _context;

    public RiskLevelCobitDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, RiskLevelCobitId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.RiskLevelCobits.AnyAsync(x => x.Code == code);
        else result = !await _context.RiskLevelCobits.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }

    public async Task<bool> IsNumericUnique(float value, RiskLevelCobitId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.RiskLevelCobits.AnyAsync(x => x.NumericValue == value);
        else result = !await _context.RiskLevelCobits.AnyAsync(x => x.NumericValue == value && x.Id != id);
        return result;
    }
}