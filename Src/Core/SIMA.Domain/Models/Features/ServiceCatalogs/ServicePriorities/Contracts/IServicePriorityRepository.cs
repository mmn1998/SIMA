using SIMA.Domain.Models.Features.ServiceCatalogs.ServicePriorities.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServicePriorities.Contracts
{
    public interface IServicePriorityRepository : IRepository<ServicePriority>
    {
        Task<ServicePriority> GetById(ServicePriorityId Id);
    }
}
