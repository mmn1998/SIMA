using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.SecurityCommitees.MeetingHoldingStatus
{
    public class DeleteMeetingHoldingStatusCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}
