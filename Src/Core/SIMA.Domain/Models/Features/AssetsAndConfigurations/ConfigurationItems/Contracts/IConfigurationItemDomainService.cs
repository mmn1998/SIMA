using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Contracts;

public interface IConfigurationItemDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ConfigurationItemId? id = null);
    Task<bool> IsVersionUnique(string version, ConfigurationItemId? id = null);
}
