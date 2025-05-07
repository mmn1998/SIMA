using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTypes.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.AssetAndConfigurations.AssetTypes;

public class AssetTypeRepository : Repository<AssetType>, IAssetTypeRepository
{
    private readonly SIMADBContext _context;

    public AssetTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<AssetType> GetById(AssetTypeId Id)
    {
        var entity = await _context.AssetTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}