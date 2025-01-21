using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.Contracts;

public interface IAssetPhysicalStatusDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, AssetPhysicalStatusId? id = null);
}