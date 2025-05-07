using SIMA.Application.Query.Contract.Features.Auths.PositionTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.PositionTypes;

public interface IPositionTypeQueryRepository : IQueryRepository
{
    Task<GetPositionTypeQueryResult> GetById(GetPositionTypeQuery request);
    Task<Result<IEnumerable<GetPositionTypeQueryResult>>> GetAll(GetAllPositionTypesQuery request);
}