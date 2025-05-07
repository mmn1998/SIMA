using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftIssueTypes.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftIssueTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.TrustyDrafts.DraftIssueTypes;

public class DraftIssueTypeDomainService : IDraftIssueTypeDomainService
{
    private readonly SIMADBContext _context;

    public DraftIssueTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, DraftIssueTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.DraftIssueTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.DraftIssueTypes.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}