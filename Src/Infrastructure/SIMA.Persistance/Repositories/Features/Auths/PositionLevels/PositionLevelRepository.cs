using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.PositionLevels.Contracts;
using SIMA.Domain.Models.Features.Auths.PositionLevels.Entities;
using SIMA.Domain.Models.Features.Auths.PositionLevels.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Auths.PositionLevels;

public class PositionLevelRepository : Repository<PositionLevel>, IPositionLevelRepository
{
    private readonly SIMADBContext _context;

    public PositionLevelRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PositionLevel> GetById(PositionLevelId Id)
    {
        var entity = await _context.PositionLevels.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}