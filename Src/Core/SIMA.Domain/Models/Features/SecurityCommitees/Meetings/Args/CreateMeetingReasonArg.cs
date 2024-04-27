namespace SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Args;

public class CreateMeetingReasonArg
{
    public long MeetingHoldingReasonId { get; set; }
    public long MeetingId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}