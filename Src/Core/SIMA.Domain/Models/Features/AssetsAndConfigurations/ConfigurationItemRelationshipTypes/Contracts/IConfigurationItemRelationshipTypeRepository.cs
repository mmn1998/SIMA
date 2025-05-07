using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes.Contracts;

public interface IConfigurationItemRelationshipTypeRepository : IRepository<ConfigurationItemRelationshipType>
{
    Task<ConfigurationItemRelationshipType> GetById(ConfigurationItemRelationshipTypeId id);
}