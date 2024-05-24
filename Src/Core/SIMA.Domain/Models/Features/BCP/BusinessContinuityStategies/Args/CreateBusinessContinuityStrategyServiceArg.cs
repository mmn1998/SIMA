namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Args;

public class CreateBusinessContinuityStrategyServiceArg
{
    public long BusinessContinuityStategyId { get; set; }
    public long? ServiceId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
