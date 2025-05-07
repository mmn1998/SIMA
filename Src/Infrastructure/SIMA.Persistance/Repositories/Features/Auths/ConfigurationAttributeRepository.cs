using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes;
using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.Entities;
using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Auths;

public class ConfigurationAttributeRepository : Repository<ConfigurationAttribute>, IConfigurationAttributeRepository
{
    private readonly SIMADBContext _context;

    public ConfigurationAttributeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ConfigurationAttribute> GetById(long id)
    {
        var entity = await _context.ConfigurationAttributes.FirstOrDefaultAsync(x => x.Id == new ConfigurationAttributeId(id));
        entity.NullCheck();
        return entity;
    }
}
