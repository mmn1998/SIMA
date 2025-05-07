using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.AssetAndConfigurations.Assets;

public class AssetRepository : Repository<Asset>, IAssetRepository
{
    private readonly SIMADBContext _context;

    public AssetRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Asset> GetById(AssetId Id)
    {
        var entity = await _context.Assets
            .Include(x => x.AssetDocuments)
            .Include(x => x.AssetCustomFieldValue)
            .Include(x => x.ServiceAssets)
            .Include(x => x.AssetAssignedStaffs)
            .Include(x => x.ConfigurationItemAssets)
            .Include(x => x.Assets) // ComplexAsset

            .FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}