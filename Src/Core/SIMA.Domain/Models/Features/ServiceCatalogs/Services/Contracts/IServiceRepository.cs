using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Contracts;

public interface IServiceRepository : IRepository<Service>
{
    Task<Service> GetById(ServiceId id);
    Task<Service?> GetLastService();
}
