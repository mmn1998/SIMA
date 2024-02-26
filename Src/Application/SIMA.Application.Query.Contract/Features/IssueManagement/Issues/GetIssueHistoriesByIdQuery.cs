using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues
{
    public class GetIssueHistoriesByIdQuery : IQuery<Result<GetIssueHistoriesByIdQueryResult>>
    {
        public long Id { get; set; }
    }
}
