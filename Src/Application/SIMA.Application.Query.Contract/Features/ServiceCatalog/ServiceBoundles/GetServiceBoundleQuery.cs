using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceBoundles;

public class GetServiceBoundleQuery : IQuery<Result<GetServiceBoundleQueryResult>>
{
    public long Id { get; set; }
}