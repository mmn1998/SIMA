using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.InherentOccurrenceProbabilities;

public class InherentOccurrenceProbabilityDomainService : IInherentOccurrenceProbabilityDomainService
{
    private readonly SIMADBContext _context;

    public InherentOccurrenceProbabilityDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, InherentOccurrenceProbabilityId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.InherentOccurrenceProbabilities.AnyAsync(x => x.Code == code);
        else result = !await _context.InherentOccurrenceProbabilities.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }
}