using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.SecurityCommitees.MeetingHoldingReasons;

public class GetMeetingHoldingReasonQuery : IQuery<Result<GetMeetingHoldingReasonQueryResult>>
{
    public long Id { get; set; }
}
