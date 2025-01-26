using SIMA.Application.Query.Contract.Features.RiskManagement.ThreatTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.TriggerStatuses;

public class GetTriggerStatusesQuery: IQuery<Result<GetTriggerStatusesQueryResult>>
{
    public long Id { get; set; }
}