using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCustomerTypes.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCustomerTypes.Entities;

public class ServiceCustomerType : Entity
{
    private ServiceCustomerType()
    {
    }
    private ServiceCustomerType(CreateServiceCustomerTypeArg arg)
    {
        Id = new ServiceCustomerTypeId(arg.Id);
        ParentId = new ServiceCustomerTypeId(arg.ParentId);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ServiceCustomerType> Create(CreateServiceCustomerTypeArg arg)
    {
        return new ServiceCustomerType(arg);
    }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public ServiceCustomerTypeId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public virtual ServiceCustomerType Parent { get; private set; }
    public ServiceCustomerTypeId? ParentId { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<ServiceCustomer> _serviceCustomers = new();
    public ICollection<ServiceCustomer> ServiceCustomers => _serviceCustomers;
}
