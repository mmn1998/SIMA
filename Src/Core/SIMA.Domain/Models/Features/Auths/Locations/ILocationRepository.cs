using SIMA.Domain.Models.Features.Auths.Locations.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.Locations;

public interface ILocationRepository : IRepository<Location>
{
    Task<Location> GetById(long id);
}
