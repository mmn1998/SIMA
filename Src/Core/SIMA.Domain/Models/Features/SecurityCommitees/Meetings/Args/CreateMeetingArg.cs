using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Args;

public class CreateMeetingArg
{
    public string? Code { get; set; }
    public int? MeetingTurn { get; set; }
    public string? Description { get; set; }
    public long? IssueId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}