using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.TrustyDrafts.DraftOrigins;

public class DraftOriginDomainService : IDraftOriginDomainService
{
    private readonly SIMADBContext _context;

    public DraftOriginDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, DraftOriginId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.DraftOrigins.AnyAsync(x => x.Code == code);
        else result = !await _context.DraftOrigins.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}