using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.AssetAndConfigurations.ConfigurationItems;

public class ConfigurationItemDomainService : IConfigurationItemDomainService
{
    private readonly SIMADBContext _context;

    public ConfigurationItemDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, ConfigurationItemId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.ConfigurationItems.AnyAsync(x => x.Code == code);
        else result = !await _context.ConfigurationItems.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }

    public async Task<bool> IsVersionUnique(string version, ConfigurationItemId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.ConfigurationItems.AnyAsync(x => x.VersionNumber == version);
        else result = !await _context.ConfigurationItems.AnyAsync(x => x.VersionNumber == version && x.Id != id);
        return result;
    }
}