namespace SIMA.Application.Contract.Features.AssetAndConfigurations.Assets;

public class CreateAssetAssignedStaffCommand
{
    public long ResponsilbeTypeId { get; set; }
    public long StaffId { get; set; }
    public long? DepartmentId { get; set; }
    public long? BranchId { get; set; }
}
