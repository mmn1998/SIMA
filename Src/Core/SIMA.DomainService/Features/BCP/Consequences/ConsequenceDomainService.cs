using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.Consequences.Contracts;
using SIMA.Domain.Models.Features.BCP.Consequences.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BCP.Consequences;

public class ConsequenceDomainService : IConsequenceDomainService
{
    private readonly SIMADBContext _context;

    public ConsequenceDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, ConsequenceId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.Consequences.AnyAsync(x => x.Code == code);
        else result = !await _context.Consequences.AnyAsync(x => x.Code == code && x.Id == Id);
        return result;
    }
}