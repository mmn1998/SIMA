using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Contracts;

public interface IConfigurationItemRepository : IRepository<ConfigurationItem>
{
    Task<ConfigurationItem> GetById(ConfigurationItemId id);
}
