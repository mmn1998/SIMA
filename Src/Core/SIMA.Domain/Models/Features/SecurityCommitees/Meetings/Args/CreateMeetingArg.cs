using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Args;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Args;

public class CreateMeetingArg
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public int? MeetingTurn { get; set; }
    public string? Description { get; set; }
    public long IssueId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public List<string> Labels { get; set; }
    public List<CreateSubjectArg> NewSubject { get; set; }
}

public class SubjectCommand
{
    public string Title { get; set; }
    public string Description { get; set; }
}