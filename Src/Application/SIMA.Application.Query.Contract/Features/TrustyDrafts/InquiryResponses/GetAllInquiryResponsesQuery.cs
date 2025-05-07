using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.InquiryResponses
{
    public class GetAllInquiryResponsesQuery : BaseRequest, IQuery<Result<IEnumerable<GetInquiryResponseQueryResult>>>
    {
    }
}
