using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.IssueManagement.IssueLinkReasons
{
    public class GetAllIssueLinkReasonsQuery : IQuery<Result<List<GetIssueLinkReasonQueryResult>>>
    {
        public BaseRequest Request { get; set; } = new();

    }
}
