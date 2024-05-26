namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;

public class ModifyBusinessContinuityPlanStaffArg
{
    public long BusinessContinuityPlanId { get; set; }
    public long StaffId { get; set; }
    public long ActiveStatusId { get; set; }

    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
