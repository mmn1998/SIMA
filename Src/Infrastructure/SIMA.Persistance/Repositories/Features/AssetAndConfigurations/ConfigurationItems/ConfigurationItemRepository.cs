using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.AssetAndConfigurations.ConfigurationItems;

public class ConfigurationItemRepository : Repository<ConfigurationItem>, IConfigurationItemRepository
{
    private readonly SIMADBContext _context;

    public ConfigurationItemRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ConfigurationItem> GetById(ConfigurationItemId Id)
    {
        var entity = await _context.ConfigurationItems
            .Include(x => x.ConfigurationItemDocuments)
            .Include(x => x.ServiceConfigurationItems)
            .Include(x => x.ConfigurationItemBackupSchedules)
            .Include(x => x.ConfigurationItemCustomFieldValues)
            .Include(x => x.ConfigurationItemAccessInfos)       
            .Include(x => x.ConfigurationItemApis)
            .Include(x => x.ConfigurationItemDataProcedures)
            .Include(x => x.ConfigurationItemAssets)
            .Include(x => x.ServiceConfigurationItems)
            .FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}