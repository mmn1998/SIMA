using SIMA.Application.Query.Contract.Features.BCP.SenarioExecutionHistories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.SenarioExecutionHistories
{
    public interface ISenarioExecutionHistoryQueryRepository : IQueryRepository
    {
        Task<GetSenarioExecutionHistoryQueryResult> GetById(GetSenarioExecutionHistoryQuery request);
        Task<Result<IEnumerable<GetSenarioExecutionHistoryQueryResult>>> GetAll(GetAllSenarioExecutionHistoriesQuery request);
    }
}
