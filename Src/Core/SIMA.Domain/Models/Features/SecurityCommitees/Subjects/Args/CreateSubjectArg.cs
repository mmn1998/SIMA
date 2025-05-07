namespace SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Args;

public class CreateSubjectArg
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? IsArchived { get; set; }
    public long? ArchivedBy { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}