using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.IssueManagement.IssueLinkReasons
{
    public class GetIssueLinkReasonQuery : IQuery<Result<GetIssueLinkReasonQueryResult>>
    {
        public long Id { get; set; }
    }
}
