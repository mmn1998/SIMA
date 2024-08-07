using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Positions.Entities;
using SIMA.Domain.Models.Features.Auths.Positions.Repositories;
using SIMA.Domain.Models.Features.Auths.Positions.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;
using SIMA.Resources;

namespace SIMA.Persistance.Repositories.Features.Auths;

public class PositionRepository : Repository<Position>, IPositionRepository
{
    private readonly SIMADBContext _context;

    public PositionRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Position> GetById(long id)
    {
        var entity = await _context.Positions.FirstOrDefaultAsync(x => x.Id == new PositionId(id));
        if (entity is null) throw new SimaResultException(CodeMessges._100054Code, Messages.PositionNotFoundError);
        return entity;
    }
}
