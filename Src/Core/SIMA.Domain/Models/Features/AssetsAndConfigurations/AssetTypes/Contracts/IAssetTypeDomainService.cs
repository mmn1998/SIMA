using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTypes.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTypes.Contracts;

public interface IAssetTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, AssetTypeId? id = null);
}