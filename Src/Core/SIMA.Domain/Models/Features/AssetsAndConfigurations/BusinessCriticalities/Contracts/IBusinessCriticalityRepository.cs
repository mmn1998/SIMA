using SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Contracts;

public interface IBusinessCriticalityRepository : IRepository<BusinessCriticality>
{
    Task<BusinessCriticality> GetById(BusinessCriticalityId id);
}