using System.Text;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetAssignedStaffs.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetAssignedStaffs.ValueObject;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Entities;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Branches.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetAssignedStaffs.Entities;

public class AssetAssignedStaff : Entity
{
     private AssetAssignedStaff()
    {

    }
    private AssetAssignedStaff(CreateAssetAssignedStaffArg arg)
    {
        Id = new(arg.Id);
        AssetId = new (arg.AssetId);
        StaffId = new (arg.StaffId);
        DepartmentId = new (arg.DepartmentId);
        BranchId = new (arg.BranchId);
        ResponsibleTypeId = new (arg.ResponsibleTypeId);
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static AssetAssignedStaff Create(CreateAssetAssignedStaffArg arg)
    {
        CreateGuards(arg);
        return new AssetAssignedStaff(arg);
    }
    public void Modify(ModifyAssetAssignedStaffArg arg)
    {
        ModifyGuards(arg);
        AssetId = new (arg.AssetId);
        StaffId = new (arg.StaffId);
        DepartmentId = new (arg.DepartmentId);
        BranchId = new (arg.BranchId);
        ResponsibleTypeId = new (arg.ResponsibleTypeId);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static void CreateGuards(CreateAssetAssignedStaffArg arg)
    {

    }
    private void ModifyGuards(ModifyAssetAssignedStaffArg arg)
    {

    }
    #endregion

    public AssetAssignedStaffId Id { get; set; }
    public AssetId AssetId { get; private set; }
    public virtual Asset Asset { get; private set; }
    public StaffId StaffId { get; private set; }
    public virtual Staff Staff { get; private set; }
    public DepartmentId DepartmentId { get; private set; }
    public virtual Department Department { get; private set; }
    public BranchId BranchId { get; private set; }
    public virtual Branch Branch { get; private set; }
    public ResponsibleTypeId ResponsibleTypeId { get; private set; }
    public virtual ResponsibleType ResponsibleType { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}