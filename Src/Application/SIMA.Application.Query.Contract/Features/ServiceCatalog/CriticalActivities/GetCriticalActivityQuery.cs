using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;


namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.CriticalActivities;

public class GetCriticalActivityQuery : IQuery<Result<GetCriticalActivityQueryResult>>
{
    public long Id { get; set; }
    public long IssueId { get; set; }
}