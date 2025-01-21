using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.AssetAndConfigurations.Assets
{
    public class AssetRepository : Repository<Asset>, IAssetRepository
    {
        private readonly SIMADBContext _context;

        public AssetRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public Task<Asset> GetById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
