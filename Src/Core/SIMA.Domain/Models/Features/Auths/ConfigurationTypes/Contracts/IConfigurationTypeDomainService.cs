using SIMA.Domain.Models.Features.Auths.ConfigurationTypes.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.ConfigurationTypes.Contracts;

public interface IConfigurationTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ConfigurationTypeId? id = null);
}