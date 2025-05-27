using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.BusinessContinuityPlans;

public class GetBusinessContinuityPlanByVersionQuery: IQuery<Result<GetBusinessContinuityPlanQueryResult>>
{
    public long BusinessContinuityPlanId { get; set; }
    public string VersionNumber { get; set; }
}