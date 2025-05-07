using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.IssueManagement.IssueWightCategories;

public class IssueWeightCategoryDomainService : IIssueWeightCategoryDomainService
{
    private readonly SIMADBContext _context;

    public IssueWeightCategoryDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, long id)
    {
        if (id > 0)
            return !await _context.IssueWeightCategories.AnyAsync(b => b.Code == code && b.Id != new IssueWeightCategoryId(id));
        else
            return !await _context.IssueWeightCategories.AnyAsync(b => b.Code == code);
    }

    public async Task<bool> IsRangeExist(int minRange, int maxRange, long id)
    {
        bool result = false;
        if (id > 0) result = await _context.IssueWeightCategories.AnyAsync(iwc => iwc.MinRange >= minRange && iwc.MaxRange <= maxRange);
        else result = await _context.IssueWeightCategories.AnyAsync(iwc => iwc.MinRange >= minRange && iwc.MaxRange <= maxRange && iwc.Id != new IssueWeightCategoryId(id));
        return result;
    }
    public bool IsRangeVilid(int minRange, int maxRange)
    {
        return minRange < maxRange;
    }
}
