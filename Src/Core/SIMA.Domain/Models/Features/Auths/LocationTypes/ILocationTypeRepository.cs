using SIMA.Domain.Models.Features.Auths.LocationTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.LocationTypes;

public interface ILocationTypeRepository : IRepository<LocationType>
{
    Task<LocationType> GetById(long id);
}
