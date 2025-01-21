using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.InquiryRequests
{
    public class GetInquiryRequestQuery : IQuery<Result<GetInquiryRequestQueryResult>>
    {
        public long Id { get; set; }
    }
}
