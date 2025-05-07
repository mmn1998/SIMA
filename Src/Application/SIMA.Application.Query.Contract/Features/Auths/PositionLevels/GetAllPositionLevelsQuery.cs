using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.PositionLevels;

public class GetAllPositionLevelsQuery : BaseRequest, IQuery<Result<IEnumerable<GetPositionLevelQueryResult>>>
{
}