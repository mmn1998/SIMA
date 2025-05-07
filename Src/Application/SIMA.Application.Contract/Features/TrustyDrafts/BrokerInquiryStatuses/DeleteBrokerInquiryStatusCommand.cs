using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.TrustyDrafts.BrokerInquiryStatuses;

public class DeleteBrokerInquiryStatusCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}