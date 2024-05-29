using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceBoundles.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Args;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Entities;

public class ServiceCategory : Entity
{
    private ServiceCategory()
    {
    }
    private ServiceCategory(CreateServiceCategoryArg arg)
    {
        Id = new ServiceCategoryId(arg.Id);
        ServiceTypeId = new ServiceTypeId(arg.ServiceTypeId);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ServiceCategory> Create(CreateServiceCategoryArg arg)
    {
        return new ServiceCategory(arg);
    }
    public ServiceCategoryId Id { get; private set; }
    public ServiceTypeId ServiceTypeId { get; private set; }
    public virtual ServiceType serviceType { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }


    private List<ServiceBoundle> _serviceBoundles = new();
    public ICollection<ServiceBoundle> ServiceBoundles => _serviceBoundles;
}
