using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.IssueManagement.IssuePriorities.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.IssueManagement.IssuePriorities;

public class IssuePriorityDomainService : IIssuePriorityDomainService
{
    private readonly SIMADBContext _context;

    public IssuePriorityDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, long id)
    {
        if (id > 0)
            return !await _context.IssuePriorities.AnyAsync(b => b.Code == code && b.Id != new IssuePriorityId(id));
        else
            return !await _context.IssuePriorities.AnyAsync(b => b.Code == code);
    }
}
