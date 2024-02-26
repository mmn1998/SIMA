using SIMA.Application.Query.Contract.Features.Auths.LocationTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.LocationTypes;

public interface ILocationTypeQueryRepository : IQueryRepository
{
    Task<GetLocationTypeQueryResult> FindById(long id);
    Task<Result<List<GetLocationTypeQueryResult>>> GetAll(BaseRequest? baseRequest = null);
}
