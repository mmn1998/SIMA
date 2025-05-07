using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.AssetAndConfigurations.Assets;

public class AssetDomainService : IAssetDomainService
{
    private readonly SIMADBContext _context;

    public AssetDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, AssetId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.Assets.AnyAsync(x => x.Code == code);
        else result = !await _context.Assets.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}