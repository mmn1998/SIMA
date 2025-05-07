using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.HappeningPossiblities.Contracts;
using SIMA.Domain.Models.Features.BCP.HappeningPossiblities.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BCP.HappeningPossiblities;

public class HappeningPossibilityDomainService : IHappeningPossibilityDomainService
{
    private readonly SIMADBContext _context;

    public HappeningPossibilityDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, HappeningPossibilityId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.HappeningPossibilities.AnyAsync(x => x.Code == code);
        else result = !await _context.HappeningPossibilities.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}
