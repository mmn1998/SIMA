using SIMA.Domain.Models.Features.ServiceCatalogs.ServicePriority.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServicePriority.Entities;

public class ServicePriority : Entity
{

    private ServicePriority()
    {
    }
    public ServicePriority(CreateServicePriorityArg arg)
    {
        Id = new ServicePriorityId(arg.Id);
        Name = arg.Name;
        Ordering = arg.Ordering;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }

    public static async Task<ServicePriority> Create(CreateServicePriorityArg arg)
    {
        return new ServicePriority(arg);
    }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public ServicePriorityId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public int Ordering { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }


    private List<Service> _services = new();
    public ICollection<Service> Services => _services;

}
