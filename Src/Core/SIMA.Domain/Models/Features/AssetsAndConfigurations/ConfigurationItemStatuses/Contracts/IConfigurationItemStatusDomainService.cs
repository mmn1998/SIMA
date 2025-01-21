using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemStatuses.Contracts;

public interface IConfigurationItemStatusDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ConfigurationItemStatusId? id = null);
}