using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.LocationTypes;
using SIMA.Domain.Models.Features.Auths.LocationTypes.Entities;
using SIMA.Domain.Models.Features.Auths.LocationTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;
using SIMA.Resources;

namespace SIMA.Persistance.Repositories.Features.Auths;

public class LocationTypeRepository : Repository<LocationType>, ILocationTypeRepository
{
    private readonly SIMADBContext _context;

    public LocationTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }
    public async Task<LocationType> GetById(long id)
    {
        var entity = await _context.LocationTypes.FirstOrDefaultAsync(x => x.Id == new LocationTypeId(id));
        if (entity is null) throw new SimaResultException("10059",Messages.LocationTypeNotFoundError);
        return entity;
    }
}
