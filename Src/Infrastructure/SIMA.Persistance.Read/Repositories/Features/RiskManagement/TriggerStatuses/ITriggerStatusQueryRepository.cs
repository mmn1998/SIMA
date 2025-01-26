using SIMA.Application.Query.Contract.Features.RiskManagement.ThreatTypes;
using SIMA.Application.Query.Contract.Features.RiskManagement.TriggerStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.TriggerStatuses;

public interface ITriggerStatusQueryRepository: IQueryRepository
{
    Task<Result<IEnumerable<GetTriggerStatusesQueryResult>>> GetAll(GetAllTriggerStatusesQuery request);
    Task<GetTriggerStatusesQueryResult> GetById(long id);
}