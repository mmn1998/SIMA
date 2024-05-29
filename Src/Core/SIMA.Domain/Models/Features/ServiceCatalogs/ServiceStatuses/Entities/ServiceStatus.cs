using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceStatuses.Args;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceStatuses.Entities;
public class ServiceStatus
{
    public ServiceStatus()
    {
    }
    public ServiceStatus(CreateServiceStatusArg arg)
    {
        Id = new ServiceStatusId(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ServiceStatus> Create(CreateServiceStatusArg arg)
    {
        return new ServiceStatus(arg);
    }
    public ServiceStatusId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long ModifiedBy { get; private set; }
}
