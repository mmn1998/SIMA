namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetAssignedStaffs.Args;

public class CreateAssetAssignedStaffArg
{
    public long Id { get; set; }
    public long AssetId { get; set; }
    public long StaffId { get; set; }
    public long? DepartmentId { get; set; }
    public long? BranchId { get; set; }
    public long ResponsilbeTypeId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}