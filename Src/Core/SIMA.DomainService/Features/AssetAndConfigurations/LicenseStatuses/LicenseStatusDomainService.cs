using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseStatuses.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseStatuses.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.AssetAndConfigurations.LicenseStatuses;

public class LicenseStatusDomainService : ILicenseStatusDomainService
{
    private readonly SIMADBContext _context;

    public LicenseStatusDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, LicenseStatusId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.LicenseStatuses.AnyAsync(x => x.Code == code);
        else result = !await _context.LicenseStatuses.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}