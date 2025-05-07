using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues
{
    public class GetIssueHistoriesByIssueIdQuery : IQuery<Result<IEnumerable<GetIssueHistoriesByIssueIdQueryResult>>>
    {
        public long IssueId { get; set; }
    }
}
