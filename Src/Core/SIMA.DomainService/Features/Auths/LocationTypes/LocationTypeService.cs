using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.LocationTypes.Interfaces;
using SIMA.Domain.Models.Features.Auths.LocationTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Auths.LocationTypes;

public class LocationTypeService : ILocationTypeService
{
    private readonly SIMADBContext _context;

    public LocationTypeService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, long id)
    {
        if (id > 0)
            return !await _context.LocationTypes.AnyAsync(b => b.Code == code && b.Id != new LocationTypeId(id));
        else
            return !await _context.LocationTypes.AnyAsync(b => b.Code == code);
    }
}
