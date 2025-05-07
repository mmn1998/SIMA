using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.PositionLevels.Contracts;
using SIMA.Domain.Models.Features.Auths.PositionLevels.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Auths.PositionLevels;

public class PositionLevelDomainService : IPositionLevelDomainService
{
    private readonly SIMADBContext _context;

    public PositionLevelDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, PositionLevelId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.PositionLevels.AnyAsync(x => x.Code == code);
        else result = !await _context.PositionLevels.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}