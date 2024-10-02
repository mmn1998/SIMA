using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.AccessTypes;

public class GetAllAccessTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetAccessTypeQueryResult>>>
{
}
