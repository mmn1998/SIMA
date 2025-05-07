using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.Contracts;

public interface IAssetPhysicalStatusRepository : IRepository<AssetPhysicalStatus>
{
    Task<AssetPhysicalStatus> GetById(AssetPhysicalStatusId id);
}