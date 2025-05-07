using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftValorStatuses.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftValorStatuses.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.TrustyDrafts.DraftValorStatuses;

public class DraftValorStatusDomainService : IDraftValorStatusDomainService
{
    private readonly SIMADBContext _context;

    public DraftValorStatusDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, DraftValorStatusId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.DraftValorStatuses.AnyAsync(x => x.Code == code);
        else result = !await _context.DraftValorStatuses.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}