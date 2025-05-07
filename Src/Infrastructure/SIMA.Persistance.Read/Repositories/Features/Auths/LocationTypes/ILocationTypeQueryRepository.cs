using SIMA.Application.Query.Contract.Features.Auths.LocationTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.LocationTypes;

public interface ILocationTypeQueryRepository : IQueryRepository
{
    Task<GetLocationTypeQueryResult> FindById(long id);
    Task<Result<IEnumerable<GetLocationTypeQueryResult>>> GetAll(GetAllLocationTypeQuery? request = null);
}
