using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.Contracts;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BCP.RecoveryPointObjectives;

public class RecoveryPointObjectiveDomainService : IRecoveryPointObjectiveDomainService
{
    private readonly SIMADBContext _context;

    public RecoveryPointObjectiveDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, RecoveryPointObjectiveId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.RecoveryPointObjectives.AnyAsync(x => x.Code == code);
        else result = !await _context.RecoveryPointObjectives.AnyAsync(x => x.Code == code && x.Id == Id);
        return result;
    }
}