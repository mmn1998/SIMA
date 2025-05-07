using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.AssetAndConfigurations.ConfigurationItemRelationshipTypes;

public class ConfigurationItemRelationshipTypeDomainService : IConfigurationItemRelationshipTypeDomainService
{
    private readonly SIMADBContext _context;

    public ConfigurationItemRelationshipTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, ConfigurationItemRelationshipTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.ConfigurationItemRelationshipTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.ConfigurationItemRelationshipTypes.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}