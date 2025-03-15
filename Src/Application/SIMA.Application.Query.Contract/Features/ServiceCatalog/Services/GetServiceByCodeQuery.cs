using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Services;

public class GetServiceByCodeQuery: IQuery<Result<GetServiceQueryResult>>
{
    public string Code { get; set; }
    public long IssueId { get; set; }
}