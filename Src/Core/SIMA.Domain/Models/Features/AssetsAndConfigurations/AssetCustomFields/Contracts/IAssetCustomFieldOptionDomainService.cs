using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Contracts;

public interface IAssetCustomFieldOptionDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, BusinessCriticalityId? id = null);
}