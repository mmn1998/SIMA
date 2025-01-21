using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.CancellationResaons.Contrcts;
using SIMA.Domain.Models.Features.TrustyDrafts.CancellationResaons.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.TrustyDrafts.CancellationResaons;

public class CancellationResaonDomainService : ICancellationResaonDomainService
{
    private readonly SIMADBContext _context;

    public CancellationResaonDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, CancellationResaonId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.CancellationResaons.AnyAsync(x => x.Code == code);
        else result = !await _context.CancellationResaons.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}