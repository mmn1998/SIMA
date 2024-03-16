using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Positions;

public class GetAllPositionsQuery : BaseRequest,  IQuery<Result<IEnumerable<GetPositionQueryResult>>>
{
}
