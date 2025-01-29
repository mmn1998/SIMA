using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.CurrentOccurrenceProbabilityValues;

public class CurrentOccurrenceProbabilityValueDomainService : ICurrentOccurrenceProbabilityValueDomainService
{
    private readonly SIMADBContext _context;

    public CurrentOccurrenceProbabilityValueDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, CurrentOccurrenceProbabilityValueId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.CurrentOccurrenceProbabilityValues.AnyAsync(x => x.Code == code);
        else result = !await _context.CurrentOccurrenceProbabilityValues.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }

    public async Task<bool> IsNumericUnique(float value, CurrentOccurrenceProbabilityValueId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.CurrentOccurrenceProbabilityValues.AnyAsync(x => x.NumericValue == value);
        else result = !await _context.CurrentOccurrenceProbabilityValues.AnyAsync(x => x.NumericValue == value && x.Id != id);
        return result;
    }
}