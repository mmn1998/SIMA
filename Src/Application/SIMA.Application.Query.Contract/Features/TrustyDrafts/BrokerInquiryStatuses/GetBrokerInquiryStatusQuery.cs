using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.BrokerInquiryStatuses;

public class GetBrokerInquiryStatusQuery : IQuery<Result<GetBrokerInquiryStatusQueryResult>>
{
    public long Id { get; set; }
}