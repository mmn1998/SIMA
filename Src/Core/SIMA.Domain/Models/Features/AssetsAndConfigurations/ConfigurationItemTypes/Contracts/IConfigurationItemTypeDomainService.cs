using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemTypes.Contracts;

public interface IConfigurationItemTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ConfigurationItemTypeId? id = null);
}