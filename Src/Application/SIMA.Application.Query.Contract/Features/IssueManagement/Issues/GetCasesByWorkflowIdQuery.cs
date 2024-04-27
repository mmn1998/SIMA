using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues;

public class GetCasesByWorkflowIdQuery : IQuery<Result<List<GetCasesByWorkflowIdQueryResult>>>
{
    public long WorkFlowId { get; set; }
}