using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.Contracts;

public interface IConfigurationItemCustomFieldRepository : IRepository<ConfigurationItemCustomField>
{
    Task<ConfigurationItemCustomField> GetById(ConfigurationItemCustomFieldId id);
}