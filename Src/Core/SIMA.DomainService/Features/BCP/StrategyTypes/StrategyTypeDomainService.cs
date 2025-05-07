using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.StrategyTypes.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BCP.StrategyTypes;

public class StrategyTypeDomainService : IStrategyTypeDomianService
{
    private readonly SIMADBContext _context;

    public StrategyTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, StrategyTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.StrategyTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.StrategyTypes.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}