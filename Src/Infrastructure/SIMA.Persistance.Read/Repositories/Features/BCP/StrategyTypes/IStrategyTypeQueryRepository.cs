using SIMA.Application.Query.Contract.Features.BCP.StrategyTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.StrategyTypes;

public interface IStrategyTypeQueryRepository : IQueryRepository
{
    Task<GetStrategyTypeQueryResult> GetById(GetStrategyTypeQuery request);
    Task<Result<IEnumerable<GetStrategyTypeQueryResult>>> GetAll(GetAllStrategyTypesQuery request);
}