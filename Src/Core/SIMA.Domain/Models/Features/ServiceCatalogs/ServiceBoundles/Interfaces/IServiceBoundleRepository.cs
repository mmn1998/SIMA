using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceBoundles.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceBoundles.Interfaces;

public interface IServiceBoundleRepository : IRepository<ServiceBoundle>
{
    Task<ServiceBoundle> GetById(ServiceBoundleId Id);
}
