using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues
{
    public class GetAllIssuesQuery : BaseRequest, IQuery<Result<IEnumerable<GetAllIssueQueryResult>>>
    {
        public long? WorkFlowId { get; set; }
        public long? ProjectId { get; set; }
        public long? DomainId { get; set; }
    }
}
