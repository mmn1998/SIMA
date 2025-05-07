using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.Contracts;

public interface IAssetTechnicalStatusRepository : IRepository<AssetTechnicalStatus>
{
    Task<AssetTechnicalStatus> GetById(AssetTechnicalStatusId id);
}