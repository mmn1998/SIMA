using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.InquiryResponses;

public class GetAllAcceptInqueryResponseQuery : IQuery<Result<IEnumerable<GetInquiryResponseQueryResult>>>
{
}
