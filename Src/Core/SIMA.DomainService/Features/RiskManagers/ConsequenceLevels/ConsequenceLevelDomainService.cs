using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.ConsequenceLevels;

public class ConsequenceLevelDomainService : IConsequenceLevelDomainService
{
    private readonly SIMADBContext _context;

    public ConsequenceLevelDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, ConsequenceLevelId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.ConsequenceLevels.AnyAsync(x => x.Code == code);
        else result = !await _context.ConsequenceLevels.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }

    public async Task<bool> IsNumericUnique(float value, ConsequenceLevelId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.ConsequenceLevels.AnyAsync(x => x.NumericValue == value);
        else result = !await _context.ConsequenceLevels.AnyAsync(x => x.NumericValue == value && x.Id != id);
        return result;
    }
}

