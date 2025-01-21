using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceCategories;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ServicePriorities
{
    public class GetAllServicePrioritiesQuery : BaseRequest, IQuery<Result<IEnumerable<GetAllServicePrioritiesQueryResult>>>
    {
    }
}
