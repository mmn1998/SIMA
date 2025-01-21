using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemStatuses.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemStatuses.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.AssetAndConfigurations.ConfigurationItemStatuses;

public class ConfigurationItemStatusRepository : Repository<ConfigurationItemStatus>, IConfigurationItemStatusRepository
{
    private readonly SIMADBContext _context;

    public ConfigurationItemStatusRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ConfigurationItemStatus> GetById(ConfigurationItemStatusId Id)
    {
        var entity = await _context.ConfigurationItemStatuses.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}