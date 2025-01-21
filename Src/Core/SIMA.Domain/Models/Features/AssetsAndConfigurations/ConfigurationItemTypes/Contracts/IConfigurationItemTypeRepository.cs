using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemTypes.Contracts;

public interface IConfigurationItemTypeRepository : IRepository<ConfigurationItemType>
{
    Task<ConfigurationItemType> GetById(ConfigurationItemTypeId id);
}