using SIMA.Domain.Models.Features.Auths.CustomerTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

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
        ServiceCustomerTypeId = new CustomerTypeId(arg.ServiceCustomerTypeId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static ServiceCustomer Create(CreateServiceCustomerArg arg)
    {
        return new ServiceCustomer(arg);
    }
    public ServiceCustomerId Id { get; private set; }
    public virtual Service Service { get; private set; }
    public ServiceId ServiceId { get; private set; }
    public virtual CustomerType ServiceCustomerType { get; private set; }
    public CustomerTypeId ServiceCustomerTypeId { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
}
