namespace SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.Args;

public class CreateSubjectPriorityArg
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public float? Ordering { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}
