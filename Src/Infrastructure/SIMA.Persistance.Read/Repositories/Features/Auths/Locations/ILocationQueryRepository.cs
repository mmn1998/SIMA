using SIMA.Application.Query.Contract.Features.Auths.Locations;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Locations;

public interface ILocationQueryRepository : IQueryRepository
{
    Task<GetLocationQueryResult> FindById(long id);
    Task<Result<IEnumerable<GetLocationQueryResult>>> GetAll(GetAllLocationQuery? request = null);
    Task<Result<IEnumerable<GetLocationQueryResult>>> GetAllCountries();
    Task<List<GetParentLocationsByLocationTypeIdQueryResult>> GetParentsByChildId(long locationTypeId);
}
