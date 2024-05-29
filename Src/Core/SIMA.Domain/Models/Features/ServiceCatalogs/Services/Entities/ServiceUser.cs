using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceUserTypes.Entities;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

public class ServiceUser : Entity
{
    private ServiceUser()
    {
    }
    private ServiceUser(CreateServiceUserArg arg)
    {
        Id = new ServiceUserId(arg.Id);
        ServiceId = new ServiceId(arg.ServiceId);
        ServiceUserTypeId = new ServiceUserTypeId(arg.ServiceUserTypeId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ServiceUser> Create(CreateServiceUserArg arg)
    {
        return new ServiceUser(arg);

    }
    public ServiceUserId Id { get; private set; }
    public virtual Service Service { get; private set; }
    public ServiceId ServiceId { get; private set; }
    public virtual ServiceUserType ServiceUserType { get; private set; }
    public ServiceUserTypeId ServiceUserTypeId { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
