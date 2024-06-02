using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceUserTypes;

public class GetAllServiceUserTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetServiceUserTypeQueryResult>>>
{
}