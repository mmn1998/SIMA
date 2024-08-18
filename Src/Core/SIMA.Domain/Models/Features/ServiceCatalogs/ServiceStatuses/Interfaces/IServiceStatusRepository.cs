using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceStatuses.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceStatuses.Interfaces
{
    public interface IServiceStatusRepository : IRepository<ServiceStatus>
    {
        Task<ServiceStatus> GetById(ServiceStatusId id);
    }
}
