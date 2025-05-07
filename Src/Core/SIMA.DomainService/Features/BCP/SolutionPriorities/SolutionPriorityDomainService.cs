using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.SolutionPriorities.Contracts;
using SIMA.Domain.Models.Features.BCP.SolutionPriorities.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BCP.SolutionPriorities;

public class SolutionPriorityDomainService : ISolutionPriorityDomainService
{
    private readonly SIMADBContext _context;

    public SolutionPriorityDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, SolutionPriorityId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.SolutionPriorities.AnyAsync(x => x.Code == code);
        else result = !await _context.SolutionPriorities.AnyAsync(x => x.Code == code && x.Id != Id && x.ActiveStatusId != 3);
        return result;
    }
}