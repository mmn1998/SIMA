using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.PlanTypes;

public class GetPlanTypeQuery : IQuery<Result<GetPlanTypeQueryResult>>
{
    public long Id { get; set; }
}