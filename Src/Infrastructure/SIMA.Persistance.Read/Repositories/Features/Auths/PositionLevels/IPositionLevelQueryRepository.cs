using SIMA.Application.Query.Contract.Features.Auths.PositionLevels;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.PositionLevels;

public interface IPositionLevelQueryRepository : IQueryRepository
{
    Task<GetPositionLevelQueryResult> GetById(GetPositionLevelQuery request);
    Task<Result<IEnumerable<GetPositionLevelQueryResult>>> GetAll(GetAllPositionLevelsQuery request);
}