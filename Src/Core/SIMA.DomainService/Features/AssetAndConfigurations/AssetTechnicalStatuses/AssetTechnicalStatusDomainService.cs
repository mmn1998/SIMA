using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.AssetAndConfigurations.AssetTechnicalStatuses;

public class AssetTechnicalStatusDomainService : IAssetTechnicalStatusDomainService
{
    private readonly SIMADBContext _context;

    public AssetTechnicalStatusDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, AssetTechnicalStatusId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.AssetTechnicalStatuses.AnyAsync(x => x.Code == code);
        else result = !await _context.AssetTechnicalStatuses.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}