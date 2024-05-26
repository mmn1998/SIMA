namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;

public class CreateBusinessContinuityPlanCriticalActivityArg
{
    public long BusinessContinuityPlanId { get; set; }
    public long CriticalActivityId { get; set; }
    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
public class ModifyBusinessContinuityPlanCriticalActivityArg
{
    public long BusinessContinuityPlanId { get; set; }
    public long CriticalActivityId { get; set; }
    public long ActiveStatusId { get; set; }

    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
}
