namespace SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Args;

internal class ModifySubjectArg
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? IsArchived { get; set; }
    public long? ArchivedBy { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
