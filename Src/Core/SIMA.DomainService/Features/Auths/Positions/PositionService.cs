using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Positions.Interfaces;
using SIMA.Domain.Models.Features.Auths.Positions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Auths.Positions;

public class PositionService : IPositionService
{
    private readonly SIMADBContext _context;

    public PositionService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, long id)
    {
        if (id > 0)
            return !await _context.Positions.AnyAsync(b => b.Code == code && b.Id != new PositionId(id));
        else
            return !await _context.Positions.AnyAsync(b => b.Code == code);
    }
}
