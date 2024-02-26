using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Positions;

public interface IPositionQueryRepository : IQueryRepository
{
    Task<GetPositionQueryResult> FindById(long id);
    Task<Result<List<GetPositionQueryResult>>> GetAll(BaseRequest? baseRequest = null);
}
