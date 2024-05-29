using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Entities;

public class ServiceType : Entity
{
    private ServiceType()
    {
    }
    private ServiceType(CreateServiceTypeArg arg)
    {
        Id = new ServiceTypeId(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ServiceType> Create(CreateServiceTypeArg arg)
    {
        return new ServiceType(arg);
    }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public ServiceTypeId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }


    private List<ServiceCategory> _serviceCategories = new();
    public ICollection<ServiceCategory> ServiceCategories => _serviceCategories;
}
