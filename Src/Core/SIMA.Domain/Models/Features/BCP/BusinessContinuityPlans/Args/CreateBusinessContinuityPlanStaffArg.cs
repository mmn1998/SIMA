namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;

public class CreateBusinessContinuityPlanStaffArg
{
    public long BusinessContinuityPlanId { get; set; }
    public long StaffId { get; set; }
    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}
