namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Args;

public class CreateBusinessContinuityStrategyObjectiveArg
{
    public long Id { get; set; }
    public long BusinessContinuityStategyId { get; set; }
    public string? Code { get; set; }
    public string? Title { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}