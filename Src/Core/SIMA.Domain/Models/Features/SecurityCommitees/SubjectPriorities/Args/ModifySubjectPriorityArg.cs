namespace SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.Args;

public class ModifySubjectPriorityArg
{
    public string? Title { get; private set; }
    public string? Description { get; private set; }
    public float? Ordering { get; private set; }
    public long ActiveStatusId { get; private set; }
    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
}
