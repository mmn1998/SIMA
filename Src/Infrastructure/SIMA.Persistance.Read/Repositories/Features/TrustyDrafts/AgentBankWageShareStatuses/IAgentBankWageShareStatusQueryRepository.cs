using SIMA.Application.Query.Contract.Features.TrustyDrafts.AgentBankWageShareStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.AgentBankWageShareStatuses;

public interface IAgentBankWageShareStatusQueryRepository : IQueryRepository
{
    Task<GetAgentBankWageShareStatusQueryResult> GetById(GetAgentBankWageShareStatusQuery request);
    Task<Result<IEnumerable<GetAgentBankWageShareStatusQueryResult>>> GetAll(GetAllAgentBankWageShareStatusesQuery request);
}
