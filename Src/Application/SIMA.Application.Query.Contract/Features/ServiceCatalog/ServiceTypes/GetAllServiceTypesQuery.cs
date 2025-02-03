using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceTypes;

public class GetAllServiceTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetServiceTypesQueryResult>>>
{
}
