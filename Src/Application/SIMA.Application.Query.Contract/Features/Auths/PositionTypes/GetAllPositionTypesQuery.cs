using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.PositionTypes;

public class GetAllPositionTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetPositionTypeQueryResult>>>
{
}