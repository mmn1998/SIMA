using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.AssetAndConfigurations.AssetPhysicalStatuses;

public class AssetPhysicalStatusRepository : Repository<AssetPhysicalStatus>, IAssetPhysicalStatusRepository
{
    private readonly SIMADBContext _context;

    public AssetPhysicalStatusRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<AssetPhysicalStatus> GetById(AssetPhysicalStatusId Id)
    {
        var entity = await _context.AssetPhysicalStatuses.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}