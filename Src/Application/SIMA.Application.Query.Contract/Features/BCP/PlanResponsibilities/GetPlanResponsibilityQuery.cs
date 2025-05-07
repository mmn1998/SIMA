using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.PlanResponsibilities;

public class GetPlanResponsibilityQuery : IQuery<Result<GetPlanResponsibilityQueryResult>>
{
    public long Id { get; set; }
}