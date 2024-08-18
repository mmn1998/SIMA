using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Services;

public class GetServiceQuery : IQuery<Result<GetServiceQueryResult>>
{
    public long Id { get; set; }
}