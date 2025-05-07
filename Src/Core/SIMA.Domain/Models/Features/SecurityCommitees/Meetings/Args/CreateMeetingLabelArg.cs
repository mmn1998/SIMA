namespace SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Args;

public class CreateMeetingLabelArg
{
    public long MeetingId { get; set; }
    public long LabelId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}