using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues;

public class GetIssueComponentQuery : IQuery<Result<GetIssueComponentQueryResult>>
{
    public long SourceId { get; set; }
    public long IssueId { get; set; }
}
