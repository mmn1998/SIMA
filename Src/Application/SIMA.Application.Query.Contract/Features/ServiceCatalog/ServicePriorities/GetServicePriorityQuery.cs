using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ServicePriorities
{
    public class GetServicePriorityQuery : IQuery<Result<GetAllServicePrioritiesQueryResult>>
    {
        public long Id { get; set; }
    }
}
