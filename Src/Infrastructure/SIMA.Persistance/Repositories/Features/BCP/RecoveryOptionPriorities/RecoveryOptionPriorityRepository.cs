using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.RecoveryOptionPriorities.Contracts;
using SIMA.Domain.Models.Features.BCP.RecoveryOptionPriorities.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BCP.RecoveryOptionPriorities;

public class RecoveryOptionPriorityRepository : Repository<RecoveryOptionPriority>, IRecoveryOptionPriorityRepository
{
    private readonly SIMADBContext _context;

    public RecoveryOptionPriorityRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<RecoveryOptionPriority> GetById(RecoveryOptionPriorityId Id)
    {
        var entity = await _context.RecoveryOptionPriorities.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}