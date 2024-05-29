using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceUserTypes.Args;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceUserTypes.Entities;

public class ServiceUserType : Entity
{
    private ServiceUserType()
    {

    }
    private ServiceUserType(CreateServiceUserTypeArg arg)
    {
        Id = new ServiceUserTypeId(arg.Id);
        ParentId = new ServiceUserTypeId(arg.ParentId);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ServiceUserType> Create(CreateServiceUserTypeArg arg)
    {
        return new ServiceUserType(arg);
    }
    public ServiceUserTypeId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public virtual ServiceUserType Parent { get; private set; }
    public ServiceUserTypeId? ParentId { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<ServiceUser> _serviceUsers = new();
    public ICollection<ServiceUser> ServiceUsers => _serviceUsers;
}
