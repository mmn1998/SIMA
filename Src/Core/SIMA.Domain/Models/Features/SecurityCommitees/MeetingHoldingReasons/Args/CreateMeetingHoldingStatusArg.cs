namespace SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.Args;

public class CreateMeetingHoldingStatusArg
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}