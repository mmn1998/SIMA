namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;

public class CreateBusinessContinuityPlanGeneralAssumptionArg
{
    public long BusinessContinuityPlanId { get; set; }
    public string? Title { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}
