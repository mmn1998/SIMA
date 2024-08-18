using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Args;
using SIMA.Framework.Core.Entities;

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
        //ResponsilbeTypeId = new ResponsilbeTypeId(arg.ResponsilbeTypeId);
        StaffId = new StaffId(arg.StaffId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;  
    }

    public async Task<CriticalActivityAssignedStaff> Create(CreateCriticalActivityAssignedStaffArg arg)
    {
        return new CriticalActivityAssignedStaff(arg);
    }
    public CriticalActivityAssignedStaffId Id { get; private set; }
    public virtual CriticalActivity CriticalActivity { get; private set; }
    public CriticalActivityId CriticalActivityId { get; private set; }
    //public virtual ResponsilbeType ResponsilbeType { get; private set; }
    //public ResponsilbeTypeId ResponsilbeTypeId { get; private set; }
    public virtual Staff Staff { get; private set; }
    public StaffId StaffId { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
