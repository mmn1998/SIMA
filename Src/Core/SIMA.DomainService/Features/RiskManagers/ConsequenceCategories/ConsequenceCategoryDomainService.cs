using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.ConsequenceCategories;


public class ConsequenceCategoryDomainService : IConsequenceCategoryDomainService
{
    private readonly SIMADBContext _context;

    public ConsequenceCategoryDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, ConsequenceCategoryId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.ConsequenceCategories.AnyAsync(x => x.Code == code);
        else result = !await _context.ConsequenceCategories.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}
