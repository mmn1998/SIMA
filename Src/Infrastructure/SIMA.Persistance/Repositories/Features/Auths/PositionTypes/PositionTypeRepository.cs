using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.PositionTypes.Contracts;
using SIMA.Domain.Models.Features.Auths.PositionTypes.Entities;
using SIMA.Domain.Models.Features.Auths.PositionTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Auths.PositionTypes;

public class PositionTypeRepository : Repository<PositionType>, IPositionTypeRepository
{
    private readonly SIMADBContext _context;

    public PositionTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PositionType> GetById(PositionTypeId Id)
    {
        var entity = await _context.PositionTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}