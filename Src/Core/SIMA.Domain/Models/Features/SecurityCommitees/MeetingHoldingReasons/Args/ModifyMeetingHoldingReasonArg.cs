namespace SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.Args;

public class ModifyMeetingHoldingReasonArg
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
