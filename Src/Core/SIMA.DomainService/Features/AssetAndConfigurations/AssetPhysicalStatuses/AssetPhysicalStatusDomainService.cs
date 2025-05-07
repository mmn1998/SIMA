using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.AssetAndConfigurations.AssetPhysicalStatuses;

public class AssetPhysicalStatusDomainService : IAssetPhysicalStatusDomainService
{
    private readonly SIMADBContext _context;

    public AssetPhysicalStatusDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, AssetPhysicalStatusId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.AssetPhysicalStatuses.AnyAsync(x => x.Code == code);
        else result = !await _context.AssetPhysicalStatuses.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}