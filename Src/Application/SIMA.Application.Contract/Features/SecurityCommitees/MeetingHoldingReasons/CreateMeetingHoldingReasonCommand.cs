using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.SecurityCommitees.MeetingHoldingReasons;

public class CreateMeetingHoldingReasonCommand : ICommand<Result<long>>
{
    public string? Code { get; set; }
    public string? Name { get; set; }
}