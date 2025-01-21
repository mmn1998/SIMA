using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTypes.Contracts;

public interface IAssetTypeRepository : IRepository<AssetType>
{
    Task<AssetType> GetById(AssetTypeId id);
}