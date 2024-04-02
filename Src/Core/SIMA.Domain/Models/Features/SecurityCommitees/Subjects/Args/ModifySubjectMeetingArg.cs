namespace SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Args;

public class ModifySubjectMeetingArg
{
    public long? SubjectId { get; set; }
    public long? MeetingId { get; set; }
    public long? SubjectPriorityId { get; set; }
    public string? IsOutOfOrder { get; set; }
    public string? IsConfirmed { get; set; }
    public long? ConfirmedBy { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
