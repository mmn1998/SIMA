namespace SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.Args;

public class CreateRecoveryPointObjectiveArg
{
    public long Id { get; set; }
    public int RpoFrom { get; set; }
    public int RpoTo { get; set; }
    public string? Name { get; set; }

    public string? Code { get; set; }

    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}