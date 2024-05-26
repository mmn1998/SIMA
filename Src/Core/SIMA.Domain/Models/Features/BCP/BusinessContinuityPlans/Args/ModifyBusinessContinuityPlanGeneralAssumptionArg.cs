namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;

public class ModifyBusinessContinuityPlanGeneralAssumptionArg
{
    public long BusinessContinuityPlanId { get; set; }
    public string? Title { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }

    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
