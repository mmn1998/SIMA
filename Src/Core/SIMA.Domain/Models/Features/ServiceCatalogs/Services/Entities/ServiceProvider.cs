using SIMA.Domain.Models.Features.Auths.Companies.Entities;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Framework.Core.Entities;
using System.Xml.Linq;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

public class ServiceProvider : Entity
{
    private ServiceProvider()
    {
    }
    private ServiceProvider(CreateServiceProviderArg arg)
    {
        Id = new ServiceProviderId(arg.Id);
        ServiceId = new ServiceId(arg.ServiceId);
        CompanyId = new CompanyId(arg.CompanyId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ServiceProvider> Create(CreateServiceProviderArg arg)
    {
        return new ServiceProvider(arg);
    }
    public ServiceProviderId Id { get; private set; }
    public virtual Service Service { get; private set; }
    public ServiceId ServiceId { get; private set; }
    public virtual Company Company { get; private set; }
    public CompanyId CompanyId { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
