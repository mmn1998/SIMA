using SIMA.Application.Query.Contract.Features.BCP.BusinessContinuityStrategies;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.BusinessContinuityStrategies;

public interface IBusinessContinuityStrategyQueryRepository : IQueryRepository
{
    Task<GetBusinessContinuityStrategyQueryResult> GetById(GetBusinessContinuityStrategyQuery request);
    Task<Result<IEnumerable<GetAllBusinessContinuityStrategiesQueryResult>>> GetAll(GetAllBusinessContinuityStrategiesQuery request);
}