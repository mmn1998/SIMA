namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;

public class ModifyBusinessContinuityPlanArg
{
    public long BusinessContinuityStrategyId { get; set; }
    public long? PlanOwnerId { get; set; }
    public long? ExecutiveResponsibleId { get; set; }
    public long? RecoveryManagerId { get; set; }
    public long? RecoveryDeputyId { get; set; }
    public DateTime OfferDate { get; set; }
    public string? Scope { get; set; }
    public string? Code { get; set; }
    public string? Title { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
