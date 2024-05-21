namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Args;

public class CreateBusinessContinuityStategyStaffArg
{
    public long BusinessContinuityStategyId { get; set; }
    public long StaffId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
public class ModifyBusinessContinuityStategyStaffArg
{
    public long BusinessContinuityStategyId { get; set; }
    public long StaffId { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
