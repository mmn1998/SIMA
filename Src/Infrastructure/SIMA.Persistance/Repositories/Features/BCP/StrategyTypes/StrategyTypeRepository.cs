using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.StrategyTypes.Contracts;
using SIMA.Domain.Models.Features.BCP.StrategyTypes.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BCP.StrategyTypes;

public class StrategyTypeRepository : Repository<StrategyType>, IStrategyTypeRepository
{
    private readonly SIMADBContext _context;

    public StrategyTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<StrategyType> GetById(StrategyTypeId Id)
    {
        var entity = await _context.StrategyTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}