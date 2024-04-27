using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.SecurityCommitees.MeetingHoldingStatus
{
    public class CreateMeetingHoldingStatusCommand : ICommand<Result<long>>
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
}
