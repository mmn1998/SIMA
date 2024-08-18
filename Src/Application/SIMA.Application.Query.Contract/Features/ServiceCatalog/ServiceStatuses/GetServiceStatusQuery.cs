using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceStatuses
{
    public class GetServiceStatusQuery : IQuery<Result<GetServiceStatusesQueryResult>>
    {
        public long Id { get; set; }
    }
}
