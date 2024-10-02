using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Entities;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;

public class CriticalActivityAssignedStaff : Entity
{
    private CriticalActivityAssignedStaff()
    {
    }

    private CriticalActivityAssignedStaff(CreateCriticalActivityAssignedStaffArg arg)
    {
        Id = new CriticalActivityAssignedStaffId(arg.Id);
        CriticalActivityId = new CriticalActivityId(arg.CriticalActivityId);
        ResponsilbeTypeId = new(arg.ResponsibleTypeId);
        StaffId = new(arg.StaffId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;  
    }

    public static CriticalActivityAssignedStaff Create(CreateCriticalActivityAssignedStaffArg arg)
    {
        return new CriticalActivityAssignedStaff(arg);
    }
    public CriticalActivityAssignedStaffId Id { get; private set; }
    public virtual CriticalActivity CriticalActivity { get; private set; }
    public CriticalActivityId CriticalActivityId { get; private set; }
    public virtual ResponsibleType ResponsilbeType { get; private set; }
    public ResponsibleTypeId ResponsilbeTypeId { get; private set; }
    public virtual Staff Staff { get; private set; }
    public StaffId StaffId { get; private set; }
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
