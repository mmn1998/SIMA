using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.Contracts;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BCP.ConsequenceIntensions;

public class ConsequenceIntensionDomainService : IConsequenceIntensionDomainService
{
    private readonly SIMADBContext _context;

    public ConsequenceIntensionDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, ConsequenceIntensionId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.ConsequenceIntensions.AnyAsync(x => x.Code == code);
        else result = !await _context.ConsequenceIntensions.AnyAsync(x => x.Code == code && x.Id != Id && x.ActiveStatusId != 3);
        return result;
    }
}
