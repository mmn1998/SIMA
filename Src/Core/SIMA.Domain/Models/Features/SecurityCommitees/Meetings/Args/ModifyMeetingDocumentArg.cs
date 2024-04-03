namespace SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Args;

public class ModifyMeetingDocumentArg
{
    public long MeetingId { get; set; }
    public long DocumentId { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
