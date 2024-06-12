using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.Contracts;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.Entities;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BCP.RecoveryPointObjectives;

public class RecoveryPointObjectiveRepository : Repository<RecoveryPointObjective>, IRecoveryPointObjectiveRepository
{
    private readonly SIMADBContext _context;

    public RecoveryPointObjectiveRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<RecoveryPointObjective> GetById(RecoveryPointObjectiveId Id)
    {
        var entity = await _context.RecoveryPointObjectives.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}