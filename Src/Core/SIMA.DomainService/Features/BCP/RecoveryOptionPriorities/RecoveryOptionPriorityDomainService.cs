using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.RecoveryOptionPriorities.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BCP.RecoveryOptionPriorities;

public class RecoveryOptionPriorityDomainService : IRecoveryOptionPriorityDomainService
{
    private readonly SIMADBContext _context;

    public RecoveryOptionPriorityDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, RecoveryOptionPriorityId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.RecoveryOptionPriorities.AnyAsync(x => x.Code == code);
        else result = !await _context.RecoveryOptionPriorities.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}