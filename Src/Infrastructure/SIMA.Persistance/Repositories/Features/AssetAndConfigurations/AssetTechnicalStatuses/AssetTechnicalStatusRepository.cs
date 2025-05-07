using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.AssetAndConfigurations.AssetTechnicalStatuses;

public class AssetTechnicalStatusRepository : Repository<AssetTechnicalStatus>, IAssetTechnicalStatusRepository
{
    private readonly SIMADBContext _context;

    public AssetTechnicalStatusRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<AssetTechnicalStatus> GetById(AssetTechnicalStatusId Id)
    {
        var entity = await _context.AssetTechnicalStatuses.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}