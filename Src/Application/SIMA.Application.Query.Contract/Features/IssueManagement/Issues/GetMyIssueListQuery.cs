using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues
{
    public class GetMyIssueListQuery : IQuery<Result<List<GetAllIssueQueryResult>>>
    {
        public BaseRequest Request { get; set; } = new();
        public long? WorkFlowId { get; set; }
    }
}
