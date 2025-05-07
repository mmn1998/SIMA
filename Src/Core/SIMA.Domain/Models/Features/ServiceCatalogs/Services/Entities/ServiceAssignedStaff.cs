using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Entities;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Branches.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

public class ServiceAssignedStaff : Entity
{
    private ServiceAssignedStaff()
    {
    }
    private ServiceAssignedStaff(CreateServiceAssignedStaffArg arg)
    {
        Id = new(arg.Id);
        ServiceId = new(arg.ServiceId);
        StaffId = new(arg.StaffId);
        if (arg.DepartmentId.HasValue) DepartmentId = new(arg.DepartmentId.Value);
        if (arg.BranchId.HasValue) BranchId = new(arg.BranchId.Value);
        ResponsibleTypeId = new(arg.ResponsibleTypeId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static ServiceAssignedStaff Create(CreateServiceAssignedStaffArg arg)
    {
        return new ServiceAssignedStaff(arg);
    }
    public ServiceAssignedStaffId Id { get; private set; }
    public virtual Service Service { get; private set; }
    public ServiceId ServiceId { get; private set; }
    public virtual Staff Staff { get; private set; }
    public StaffId StaffId { get; private set; }
    public virtual ResponsibleType ResponsibleType { get; private set; }
    public ResponsibleTypeId ResponsibleTypeId { get; private set; }
    public virtual Department? Department { get; private set; }
    public DepartmentId? DepartmentId { get; private set; }
    public virtual Branch? Branch { get; private set; }
    public BranchId? BranchId { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
}
