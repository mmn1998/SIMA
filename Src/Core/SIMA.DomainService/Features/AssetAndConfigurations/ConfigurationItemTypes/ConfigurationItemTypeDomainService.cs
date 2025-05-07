using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemTypes.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.AssetAndConfigurations.ConfigurationItemTypes;

public class ConfigurationItemTypeDomainService : IConfigurationItemTypeDomainService
{
    private readonly SIMADBContext _context;

    public ConfigurationItemTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, ConfigurationItemTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.ConfigurationItemTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.ConfigurationItemTypes.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}