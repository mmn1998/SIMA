namespace SIMA.Domain.Models.Features.RiskManagement.Consequences.Args;

public class ModifyRiskConsequenceArg
{
    public long Id { get; set; }
    public long ActiveStatusId { get; set; }
    public long ConsequenceCategoryId { get; set; }
    public long ConsequenceLevelId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long ModifiedBy { get; set; }
}