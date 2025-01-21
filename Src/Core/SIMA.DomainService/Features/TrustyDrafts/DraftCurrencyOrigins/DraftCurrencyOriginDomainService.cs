using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftCurrencyOrigins.Contrcts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftCurrencyOrigins.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.TrustyDrafts.DraftCurrencyOrigins;

public class DraftCurrencyOriginDomainService : IDraftCurrencyOriginDomainService
{
    private readonly SIMADBContext _context;

    public DraftCurrencyOriginDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, DraftCurrencyOriginId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.DraftCurrencyOrigins.AnyAsync(x => x.Code == code);
        else result = !await _context.DraftCurrencyOrigins.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}