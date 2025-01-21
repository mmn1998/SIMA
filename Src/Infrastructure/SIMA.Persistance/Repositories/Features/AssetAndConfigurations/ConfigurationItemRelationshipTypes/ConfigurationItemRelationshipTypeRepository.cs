using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.AssetAndConfigurations.ConfigurationItemRelationshipTypes;

public class ConfigurationItemRelationshipTypeRepository : Repository<ConfigurationItemRelationshipType>, IConfigurationItemRelationshipTypeRepository
{
    private readonly SIMADBContext _context;

    public ConfigurationItemRelationshipTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ConfigurationItemRelationshipType> GetById(ConfigurationItemRelationshipTypeId Id)
    {
        var entity = await _context.ConfigurationItemRelationshipTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}