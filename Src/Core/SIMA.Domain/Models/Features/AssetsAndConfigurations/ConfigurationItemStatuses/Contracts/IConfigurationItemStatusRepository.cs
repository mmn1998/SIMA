using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemStatuses.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemStatuses.Contracts;

public interface IConfigurationItemStatusRepository : IRepository<ConfigurationItemStatus>
{
    Task<ConfigurationItemStatus> GetById(ConfigurationItemStatusId id);
}