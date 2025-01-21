using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.Origins;

public class GetAllOriginsQuery : BaseRequest, IQuery<Result<IEnumerable<GetOriginQueryResult>>>
{
}