using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.Vulnerabilities;

public class VulnerabilitiesDomainService : IThreatTypeDomainService
{
    private readonly SIMADBContext _context;

    public VulnerabilitiesDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, long id)
    {
        if (id > 0)
            return !await _context.ThreatTypes.AnyAsync(b => b.Code == code && b.Id != new ThreatTypeId(id));
        else
            return !await _context.ThreatTypes.AnyAsync(b => b.Code == code);
    }
}