using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Contracts
{
    public interface IAssetRepository : IRepository<Asset>
    {
        Task<Asset> GetById(long id);
    }
}
