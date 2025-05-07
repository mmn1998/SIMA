using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Services;

public class GetAllServicesQuery : BaseRequest, IQuery<Result<IEnumerable<GetAllServicesQueryResult>>>
{
}
