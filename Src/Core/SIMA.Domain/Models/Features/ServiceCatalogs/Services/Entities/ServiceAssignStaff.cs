using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Framework.Core.Entities;
using System.Xml.Linq;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

public class ServiceAssignStaff : Entity
{
    private ServiceAssignStaff()
    {
    }
    private ServiceAssignStaff(CreateServiceAssignStaffArg arg)
    {
        Id = new ServiceAssignStaffId(arg.Id);
        ServiceId = new ServiceId(arg.ServiceId);
        StaffId = new StaffId(arg.ServiceId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ServiceAssignStaff> Create(CreateServiceAssignStaffArg arg)
    {
        return new ServiceAssignStaff(arg);
    }
    public ServiceAssignStaffId Id { get; private set; }
    public virtual Service Service { get; private set; }
    public ServiceId ServiceId { get; private set; }
    public virtual Staff Staff { get; private set; }
    public StaffId StaffId { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

}
