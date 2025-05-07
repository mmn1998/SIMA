using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.PlanResponsibilities;

public class GetAllPlanResponsibilitiesQuery : BaseRequest, IQuery<Result<IEnumerable<GetPlanResponsibilityQueryResult>>>
{
}