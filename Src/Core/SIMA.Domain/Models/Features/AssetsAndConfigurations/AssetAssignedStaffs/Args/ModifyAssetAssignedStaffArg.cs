namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetAssignedStaffs.Args;

public class ModifyAssetAssignedStaffArg
{
    public long Id { get; set; }
    public long AssetId { get; set; }
    public long StaffId { get; set; }
    public long? DepartmentId { get; set; }
    public long? BranchId { get; set; }
    public long ResponsilbeTypeId { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}