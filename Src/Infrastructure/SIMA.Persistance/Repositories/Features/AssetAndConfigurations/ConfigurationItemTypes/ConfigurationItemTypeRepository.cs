using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemTypes.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemTypes.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.AssetAndConfigurations.ConfigurationItemTypes;

public class ConfigurationItemTypeRepository : Repository<ConfigurationItemType>, IConfigurationItemTypeRepository
{
    private readonly SIMADBContext _context;

    public ConfigurationItemTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ConfigurationItemType> GetById(ConfigurationItemTypeId Id)
    {
        var entity = await _context.ConfigurationItemTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}