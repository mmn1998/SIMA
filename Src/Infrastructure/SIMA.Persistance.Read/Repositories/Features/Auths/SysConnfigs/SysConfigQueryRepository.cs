using Microsoft.EntityFrameworkCore;
using SIMA.Application.Query.Contract.Features.Auths.SysConfigs;
using SIMA.Domain.Models.Features.Auths.SysConfigs.Entities;
using SIMA.Domain.Models.Features.Auths.SysConfigs.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.SysConnfigs;

public class SysConfigQueryRepository : ISysConfigQueryRepository
{
    private readonly SIMADBContext _context;

    public SysConfigQueryRepository(SIMADBContext context)
    {
        _context = context;
    }

    public async Task<SysConfig> FindById(long id)
    {
        var result = await _context.SysConfigs.FirstOrDefaultAsync(c => c.Id == new SysConfigId(id));
        result.NullCheck();
        return result;
    }

    public async Task<List<GetSysConfigQueryResult>> GetAll()
    {
        return await _context.SysConfigs.AsNoTracking().Select(s => new GetSysConfigQueryResult
        {
            Id = s.Id.Value,
            ActiveStatusId = s.ActiveStatusId,
            ConfigurationId = s.ConfigurationId.Value,
            KeyValue = s.KeyValue
        }).ToListAsync();
    }
}
