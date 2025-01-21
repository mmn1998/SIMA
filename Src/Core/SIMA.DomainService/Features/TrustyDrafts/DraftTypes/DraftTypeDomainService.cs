using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftTypes.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.TrustyDrafts.DraftTypes;

public class DraftTypeDomainService : IDraftTypeDomainService
{
    private readonly SIMADBContext _context;

    public DraftTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, DraftTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.DraftTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.DraftTypes.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}