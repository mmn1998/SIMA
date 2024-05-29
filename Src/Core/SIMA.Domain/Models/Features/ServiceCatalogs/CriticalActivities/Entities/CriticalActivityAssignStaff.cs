using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Args;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;

public class CriticalActivityAssignStaff : Entity
{
    private CriticalActivityAssignStaff()
    {
    }

    private CriticalActivityAssignStaff(CreateCriticalActivityAssignStaffArg arg)
    {
        Id = new CriticalActivityAssignStaffId(arg.Id);
        CriticalActivityId = new(arg.CriticalActivityId);
        StaffId = new(arg.StaffId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public async Task<CriticalActivityAssignStaff> Create(CreateCriticalActivityAssignStaffArg arg)
    {
        return new CriticalActivityAssignStaff(arg);
    }
    public CriticalActivityAssignStaffId Id { get; private set; }
    public virtual CriticalActivity CriticalActivity { get; private set; }
    public CriticalActivityId CriticalActivityId { get; private set; }
    public virtual Staff Staff { get; private set; }
    public StaffId StaffId { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
