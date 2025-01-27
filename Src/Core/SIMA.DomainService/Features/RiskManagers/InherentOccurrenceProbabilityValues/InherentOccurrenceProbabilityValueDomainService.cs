using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.InherentOccurrenceProbabilityValues;

public class InherentOccurrenceProbabilityValueDomainService : IInherentOccurrenceProbabilityValueDomainService
{
    private readonly SIMADBContext _context;

    public InherentOccurrenceProbabilityValueDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, InherentOccurrenceProbabilityValueId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.InherentOccurrenceProbabilityValues.AnyAsync(x => x.Code == code);
        else result = !await _context.InherentOccurrenceProbabilityValues.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }

    public async Task<bool> IsNumericUnique(float value, InherentOccurrenceProbabilityValueId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.InherentOccurrenceProbabilityValues.AnyAsync(x => x.NumericValue == value);
        else result = !await _context.InherentOccurrenceProbabilityValues.AnyAsync(x => x.NumericValue == value && x.Id != id);
        return result;
    }
}