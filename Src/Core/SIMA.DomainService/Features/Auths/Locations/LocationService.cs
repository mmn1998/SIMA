using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Locations.Interfaces;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Auths.Locations;

public class LocationService : ILocationService
{
    private readonly SIMADBContext _context;

    public LocationService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, long id)
    {
        if (id > 0)
            return !await _context.Locations.AnyAsync(b => b.Code == code && b.Id != new LocationId(id));
        else
            return !await _context.Locations.AnyAsync(b => b.Code == code);
    }
}
