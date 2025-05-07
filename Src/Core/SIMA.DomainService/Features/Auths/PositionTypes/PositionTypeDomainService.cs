using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.PositionTypes.Contracts;
using SIMA.Domain.Models.Features.Auths.PositionTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Auths.PositionTypes;

public class PositionTypeDomainService : IPositionTypeDomainService
{
    private readonly SIMADBContext _context;

    public PositionTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, PositionTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.PositionTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.PositionTypes.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}