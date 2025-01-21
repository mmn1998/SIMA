namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Args;

public class ModifyBusinessContinuityStategyArg
{
    public long CordinatorId { get; set; }
    public long StrategyTypeId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Code { get; set; }
    public DateTime? ExpireDate { get; set; }
    public DateTime? ReviewDate { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
