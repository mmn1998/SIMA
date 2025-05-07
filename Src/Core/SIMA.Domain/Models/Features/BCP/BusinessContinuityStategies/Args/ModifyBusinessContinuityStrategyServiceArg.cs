namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Args;

public class ModifyBusinessContinuityStrategyServiceArg
{
    public long BusinessContinuityStategyId { get; set; }
    public long? ServiceId { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
