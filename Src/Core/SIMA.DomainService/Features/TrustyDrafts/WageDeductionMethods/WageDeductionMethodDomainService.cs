using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.TrustyDrafts.WageDeductionMethods;

public class WageDeductionMethodDomainService : IWageDeductionMethodDomainService
{
    private readonly SIMADBContext _context;

    public WageDeductionMethodDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, WageDeductionMethodId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.WageDeductionMethods.AnyAsync(x => x.Code == code);
        else result = !await _context.WageDeductionMethods.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}