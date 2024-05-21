namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Args;

public class ModifyBusinessContinuityStrategyObjectiveArg
{
    public long BusinessContinuityStategyId { get; set; }
    public string? Code { get; set; }
    public string? Title { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
