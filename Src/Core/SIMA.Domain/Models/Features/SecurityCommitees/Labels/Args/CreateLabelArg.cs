namespace SIMA.Domain.Models.Features.SecurityCommitees.Labels.Args;

public class CreateLabelArg
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public bool IsNew { get; set; }
}