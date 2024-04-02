namespace SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Args;

public class CreateSubjectMeetingArg
{
    public long? SubjectId { get; set; }
    public long? MeetingId { get; set; }
    public long? SubjectPriorityId { get; set; }
    public string? IsOutOfOrder { get; set; }
    public string? IsConfirmed { get; set; }
    public long? ConfirmedBy { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}