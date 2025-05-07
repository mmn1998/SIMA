using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemStatuses.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.AssetAndConfigurations.ConfigurationItemStatuses;

public class ConfigurationItemStatusDomainService : IConfigurationItemStatusDomainService
{
    private readonly SIMADBContext _context;

    public ConfigurationItemStatusDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, ConfigurationItemStatusId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.ConfigurationItemStatuses.AnyAsync(x => x.Code == code);
        else result = !await _context.ConfigurationItemStatuses.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}