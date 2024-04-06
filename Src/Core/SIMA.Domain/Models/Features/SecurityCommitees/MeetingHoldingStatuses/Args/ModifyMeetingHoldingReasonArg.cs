namespace SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.Args;

public class ModifyMeetingHoldingReasonArg
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
