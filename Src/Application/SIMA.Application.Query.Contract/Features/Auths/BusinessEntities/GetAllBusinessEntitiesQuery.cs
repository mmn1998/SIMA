using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.BusinessEntities;

public class GetAllBusinessEntitiesQuery : BaseRequest, IQuery<Result<IEnumerable<GetBusinessEntityQueryResult>>>
{
}
