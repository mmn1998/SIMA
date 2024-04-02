namespace SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Args;

public class ModifyMeetingArg
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public int? MeetingTurn { get; set; }
    public string? Description { get; set; }
    public long? IssueId { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
