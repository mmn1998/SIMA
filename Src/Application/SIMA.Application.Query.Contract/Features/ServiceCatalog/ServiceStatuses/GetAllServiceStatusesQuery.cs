using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceStatuses
{
    public class GetAllServiceStatusesQuery: BaseRequest, IQuery<Result<IEnumerable<GetServiceStatusesQueryResult>>>
    {
    }
}
