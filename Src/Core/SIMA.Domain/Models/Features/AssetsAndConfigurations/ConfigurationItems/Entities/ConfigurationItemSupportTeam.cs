using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Branches.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;

public class ConfigurationItemSupportTeam : Entity
{
    private ConfigurationItemSupportTeam() { }
    private ConfigurationItemSupportTeam(CreateConfigurationItemSupportTeamArg arg)
    {
        Id = new(arg.Id);
        ConfigurationItemId = new(arg.ConfigurationItemId);
        MainStaffId = new(arg.MainStaffId);
        SubsitutedStaffId = new(arg.SubsitutedStaffId);
        if (arg.MainDepartmentId.HasValue) MainDepartmentId = new(arg.MainDepartmentId.Value);
        if (arg.MainBranchId.HasValue) MainDepartmentId = new(arg.MainBranchId.Value);
        if (arg.SubsitutedBranchId.HasValue) MainDepartmentId = new(arg.SubsitutedBranchId.Value);
        if (arg.SubsitutedDepartmentId.HasValue) MainDepartmentId = new(arg.SubsitutedDepartmentId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static ConfigurationItemSupportTeam Create(CreateConfigurationItemSupportTeamArg arg)
    {
        return new ConfigurationItemSupportTeam(arg);
    }
    public ConfigurationItemSupportTeamId Id { get; private set; }
    public ConfigurationItemId ConfigurationItemId { get; private set; }
    public virtual ConfigurationItem ConfigurationItem { get; private set; }
    public StaffId MainStaffId { get; private set; }
    public Staff MainStaff { get; private set; }
    public DepartmentId? MainDepartmentId { get; private set; }
    public virtual Department? MainDepartment { get; private set; }
    public BranchId? MainBranchId { get; private set; }
    public virtual Branch? MainBranch { get; private set; }
    public StaffId SubsitutedStaffId { get; private set; }
    public virtual Staff SubsitutedStaff { get; private set; }
    public virtual Department? SubsitutedDepartment { get; private set; }
    public DepartmentId? SubsitutedDepartmentId { get; private set; }
    public virtual Branch? SubsitutedBranch { get; private set; }
    public virtual BranchId? SubsitutedBranchId { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}