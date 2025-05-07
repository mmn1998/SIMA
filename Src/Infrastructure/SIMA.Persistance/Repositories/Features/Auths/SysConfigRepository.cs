using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.SysConfigs.Entities;
using SIMA.Domain.Models.Features.Auths.SysConfigs.Interfaces;
using SIMA.Domain.Models.Features.Auths.SysConfigs.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Auths;

public class SysConfigRepository : Repository<SysConfig>, ISysConfigRepository
{
    private readonly SIMADBContext _context;

    public SysConfigRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<SysConfig> GetById(long id)
    {
        var entity = await _context.SysConfigs.FirstOrDefaultAsync(c => c.Id == new SysConfigId(id));
        entity.NullCheck();
        return entity;
    }
}
