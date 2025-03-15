using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTypes.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.AssetAndConfigurations.AssetTypes;

public class AssetTypeDomainService : IAssetTypeDomainService
{
    private readonly SIMADBContext _context;

    public AssetTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, AssetTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.AssetTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.AssetTypes.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}