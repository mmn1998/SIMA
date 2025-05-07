using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftReviewResults.Contrcts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftReviewResults.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.TrustyDrafts.DraftReviewResults;

public class DraftReviewResultDomainService : IDraftReviewResultDomainService
{
    private readonly SIMADBContext _context;

    public DraftReviewResultDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, DraftReviewResultId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.DraftReviewResults.AnyAsync(x => x.Code == code);
        else result = !await _context.DraftReviewResults.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}