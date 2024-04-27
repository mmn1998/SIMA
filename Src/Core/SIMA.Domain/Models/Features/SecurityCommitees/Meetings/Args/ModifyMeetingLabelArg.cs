namespace SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Args;

public class ModifyMeetingLabelArg
{
    public long MeetingId { get; set; }
    public long LabelId { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
