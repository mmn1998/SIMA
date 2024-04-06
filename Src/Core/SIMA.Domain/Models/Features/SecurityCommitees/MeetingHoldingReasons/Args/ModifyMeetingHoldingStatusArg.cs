namespace SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.Args;

public class ModifyMeetingHoldingStatusArg
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}