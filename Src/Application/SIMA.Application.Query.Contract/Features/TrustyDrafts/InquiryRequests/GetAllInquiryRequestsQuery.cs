using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.InquiryRequests
{
    public class GetAllInquiryRequestsQuery : BaseRequest, IQuery<Result<IEnumerable<GetInquiryRequestQueryResult>>>
    {
    }
}
