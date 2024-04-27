using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.SecurityCommitees.SubjectPriorities;

public class SubjectPriorityDomainService : ISubjectPriorityDomainService
{
    private readonly SIMADBContext _context;

    public SubjectPriorityDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string Code, long Id)
    {
        bool result = false;
        if (Id > 0)
            result = !await _context.SubjectPriorities.AnyAsync(b => b.Code == Code && b.Id != new SubjectPriorityId(Id));
        else
            result = !await _context.SubjectPriorities.AnyAsync(b => b.Code == Code);
        return result;
    }
}
