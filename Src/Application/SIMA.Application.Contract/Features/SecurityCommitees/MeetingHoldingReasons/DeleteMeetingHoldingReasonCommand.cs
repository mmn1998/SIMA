using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.SecurityCommitees.MeetingHoldingReasons;

public class DeleteMeetingHoldingReasonCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
