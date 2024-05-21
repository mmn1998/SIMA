using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Locations;
using SIMA.Domain.Models.Features.Auths.Locations.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;
using SIMA.Resources;

namespace SIMA.Persistance.Repositories.Features.Auths;

public class LocationRepository : Repository<Location>, ILocationRepository
{
    private readonly SIMADBContext _context;

    public LocationRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Location> GetById(long id)
    {
        var entity = await _context.Locations
        .FirstOrDefaultAsync(x => x.Id == new LocationId(id));
        if (entity is null) throw new SimaResultException("10060",Messages.LocationNotFoundError);
        return entity;
    }
}
