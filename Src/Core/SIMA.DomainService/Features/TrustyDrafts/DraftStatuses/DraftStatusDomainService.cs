using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftStatuses.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftStatuses.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.TrustyDrafts.DraftStatuses;

public class DraftStatusDomainService : IDraftStatusDomainService
{
    private readonly SIMADBContext _context;

    public DraftStatusDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, DraftStatusId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.DraftStatuses.AnyAsync(x => x.Code == code);
        else result = !await _context.DraftStatuses.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}