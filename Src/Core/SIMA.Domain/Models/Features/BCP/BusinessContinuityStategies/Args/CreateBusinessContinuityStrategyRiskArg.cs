namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Args;

public class CreateBusinessContinuityStrategyRiskArg
{
    public long BusinessContinuityStategyId { get; set; }
    public long? RiskId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}