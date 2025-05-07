using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.CurrentOccurrenceProbabilities;

public class CurrentOccurrenceProbabilityDomainService : ICurrentOccurrenceProbabilityDomainService
{
    private readonly SIMADBContext _context;

    public CurrentOccurrenceProbabilityDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, CurrentOccurrenceProbabilityId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.CurrentOccurrenceProbabilities.AnyAsync(x => x.Code == code);
        else result = !await _context.CurrentOccurrenceProbabilities.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }
}