using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.ServiceCatalog.CriticalActivities;

public class CriticalActivityDomainService : ICriticalActivityDomainService
{
    private readonly SIMADBContext _context;

    public CriticalActivityDomainService(SIMADBContext context)
    {
        _context = context;
    }

    public async Task<string?> GetLastCode()
    {
        var entity = await _context.CriticalActivities.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
        return entity?.Code;
    }

    public async Task<bool> IsCodeUnique(string code, CriticalActivityId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.CriticalActivities.AnyAsync(x => x.Code == code);
        else result = !await _context.CriticalActivities.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}