using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.Contracts;

public interface IAssetTechnicalStatusDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, AssetTechnicalStatusId? id = null);
}