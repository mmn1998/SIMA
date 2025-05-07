namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Args;

public class ModifyBusinessContinuityStrategyRiskArg
{
    public long BusinessContinuityStategyId { get; set; }
    public long? RiskId { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}