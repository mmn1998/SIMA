using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceStatuses.Interfaces
{
    public interface IServiceStatusDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, ServiceStatusId? id = null);
    }
}
