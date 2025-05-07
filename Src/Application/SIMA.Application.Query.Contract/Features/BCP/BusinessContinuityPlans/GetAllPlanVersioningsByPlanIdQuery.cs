using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.BusinessContinuityPlans;

public class GetAllPlanVersioningsByPlanIdQuery : IQuery<Result<IEnumerable<GetAllPlanVersioningsByPlanIdQueryResult>>>
{
    public long BusinessContinuityPlanId { get; set; }
}