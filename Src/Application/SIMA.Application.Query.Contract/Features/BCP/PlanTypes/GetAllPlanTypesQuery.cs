using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.PlanTypes;

public class GetAllPlanTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetPlanTypeQueryResult>>>
{
}