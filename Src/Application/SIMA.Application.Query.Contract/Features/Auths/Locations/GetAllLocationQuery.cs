using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Locations;

public class GetAllLocationQuery : BaseRequest, IQuery<Result<IEnumerable<GetLocationQueryResult>>>
{
}