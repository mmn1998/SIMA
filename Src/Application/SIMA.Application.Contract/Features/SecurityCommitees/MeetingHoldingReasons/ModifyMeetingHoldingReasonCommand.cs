using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.SecurityCommitees.MeetingHoldingReasons;

public class ModifyMeetingHoldingReasonCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
}
