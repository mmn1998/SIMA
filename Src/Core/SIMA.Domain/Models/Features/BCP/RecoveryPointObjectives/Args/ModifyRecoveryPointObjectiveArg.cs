namespace SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.Args;

public class ModifyRecoveryPointObjectiveArg
{
    public int RpoFrom { get; set; }
    public int RpoTo { get; set; }
    public string? Name { get; set; }

    public string? Code { get; set; }

    public long ActiveStatusId { get; set; }

    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
