using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.RiskValues.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.RiskValues.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.RiskValues;

public class RiskValueDomainService : IRiskValueDomainService
{
    private readonly SIMADBContext _context;

    public RiskValueDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, RiskValueId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.RiskValues.AnyAsync(x => x.Code == code);
        else result = !await _context.RiskValues.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }

    public async Task<bool> IsNumericUnique(float value, RiskValueId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.RiskValues.AnyAsync(x => x.NumericValue == value);
        else result = !await _context.RiskValues.AnyAsync(x => x.NumericValue == value && x.Id != id);
        return result;
    }
}
