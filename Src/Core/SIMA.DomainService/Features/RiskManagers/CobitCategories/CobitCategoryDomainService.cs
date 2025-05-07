using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.CobitCategories.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.CobitCategories.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.CobitCategories;

public class CobitCategoryDomainService : ICobitCategoryDomainService
{
    private readonly SIMADBContext _context;

    public CobitCategoryDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, CobitCategoryId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.CobitCategories.AnyAsync(x => x.Code == code);
        else result = !await _context.CobitCategories.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}