using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.SecurityCommitees.MeetingHoldingStatus
{
    public class GetMeetingHoldingStatusQuery : IQuery<Result<GetMeetingHoldingStatusQueryResult>>
    {
        public long Id { get; set; }
    }
}
