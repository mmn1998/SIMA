namespace SIMA.Domain.Models.Features.SecurityCommitees.Labels.Args;

public class ModifyLabelArg
{
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
