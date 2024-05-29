using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Entities;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceBoundles.Entities;

public class ServiceBoundle : Entity
{
    public ServiceBoundleId Id { get; private set; }
    public ServiceCategoryId ServiceCategoryId { get; private set; }
    public virtual ServiceCategory ServiceCategory { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<Service> _services = new();
    public ICollection<Service> Services => _services;
}
