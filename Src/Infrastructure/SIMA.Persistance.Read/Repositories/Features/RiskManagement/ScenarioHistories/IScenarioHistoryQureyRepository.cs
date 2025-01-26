using SIMA.Application.Query.Contract.Features.RiskManagement.RiskTypes;
using SIMA.Application.Query.Contract.Features.RiskManagement.ScenarioHistories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.ScenarioHistories;

public interface IScenarioHistoryQureyRepository: IQueryRepository
{
    Task<Result<IEnumerable<GetScenarioHistoryQueryResult>>> GetAll(GetAllScenarioHistoryQuery request);
    Task<GetScenarioHistoryQueryResult> GetById(long id);
}