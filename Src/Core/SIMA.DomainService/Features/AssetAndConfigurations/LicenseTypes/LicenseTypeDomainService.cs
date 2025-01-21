using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.AssetAndConfigurations.LicenseTypes;

public class LicenseTypeDomainService : ILicenseTypeDomainService
{
    private readonly SIMADBContext _context;

    public LicenseTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, LicenseTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.LicenseTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.LicenseTypes.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}