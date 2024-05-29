using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCustomerTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

public class ServiceCustomer : Entity
{
    private ServiceCustomer()
    {
    }
    private ServiceCustomer(CreateServiceCustomerArg arg)
    {
        Id = new ServiceCustomerId(arg.Id);
        ServiceId = new ServiceId(arg.ServiceId);
        ServiceCustomerTypeId = new ServiceCustomerTypeId(arg.ServiceCustomerTypeId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ServiceCustomer> Create(CreateServiceCustomerArg arg)
    {
        return new ServiceCustomer(arg);
    }
    public ServiceCustomerId Id { get; private set; }
    public virtual Service Service { get; private set; }
    public ServiceId ServiceId { get; private set; }
    public virtual ServiceCustomerType ServiceCustomerType { get; private set; }
    public ServiceCustomerTypeId ServiceCustomerTypeId { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
