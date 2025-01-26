using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.TriggerStatuses.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.TriggerStatuses.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.TriggerStatuses;

public class TriggerStatuseDomainService : ITriggerStatusDomainService
{
    private readonly SIMADBContext _context;

    public TriggerStatuseDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, TriggerStatusId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.TriggerStatuses.AnyAsync(x => x.Code == code);
        else result = !await _context.TriggerStatuses.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }
}