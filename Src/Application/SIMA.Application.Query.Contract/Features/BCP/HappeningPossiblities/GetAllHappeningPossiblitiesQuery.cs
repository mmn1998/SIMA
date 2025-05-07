using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.HappeningPossiblities;

public class GetAllHappeningPossiblitiesQuery : BaseRequest, IQuery<Result<IEnumerable<GetHappeningPossibilityQueryResult>>>
{
}