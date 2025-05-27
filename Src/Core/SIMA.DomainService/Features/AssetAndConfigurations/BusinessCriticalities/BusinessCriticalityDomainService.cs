using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.AssetAndConfigurations.BusinessCriticalities;

public class BusinessCriticalityDomainService : IBusinessCriticalityDomainService
{
    private readonly SIMADBContext _context;

    public BusinessCriticalityDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, BusinessCriticalityId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.BusinessCriticalities.AnyAsync(x => x.Code == code);
        else result = !await _context.BusinessCriticalities.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
    
    public async Task<bool> IsOrderingUnique(float? ordering, BusinessCriticalityId? Id = null)
    {
        if(ordering is null) return false;
        bool result = false;
        if (Id == null) result = !await _context.BusinessCriticalities.AnyAsync(x => x.Ordering == ordering);
        else result = !await _context.BusinessCriticalities.AnyAsync(x => x.Ordering == ordering && x.Id != Id);
        return result;
    }
    
}