using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Contracts;

public interface IAssetDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, AssetId? id = null);
}