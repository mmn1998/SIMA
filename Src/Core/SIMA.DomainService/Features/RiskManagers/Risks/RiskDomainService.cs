using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.Risks;

public class RiskDomainService : IRiskDomainService
{
    private readonly SIMADBContext _context;

    public RiskDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, RiskId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.Risks.AnyAsync(x => x.Code == code);
        else result = !await _context.Risks.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}