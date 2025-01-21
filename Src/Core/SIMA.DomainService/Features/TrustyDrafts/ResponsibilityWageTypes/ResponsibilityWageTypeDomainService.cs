using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.ResponsibilityWageTypes.Contrcts;
using SIMA.Domain.Models.Features.TrustyDrafts.ResponsibilityWageTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.TrustyDrafts.ResponsibilityWageTypes;

public class ResponsibilityWageTypeDomainService : IResponsibilityWageTypeDomainService
{
    private readonly SIMADBContext _context;

    public ResponsibilityWageTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, ResponsibilityWageTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.ResponsibilityWageTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.ResponsibilityWageTypes.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}