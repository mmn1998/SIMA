using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.SeverityValues;

public class SeverityValueDomainService : ISeverityValueDomainService
{
    private readonly SIMADBContext _context;

    public SeverityValueDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, SeverityValueId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.SeverityValues.AnyAsync(x => x.Code == code);
        else result = !await _context.SeverityValues.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }

    public async Task<bool> IsNumericUnique(float value, SeverityValueId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.SeverityValues.AnyAsync(x => x.NumericValue == value && x.ActiveStatusId == (long)ActiveStatusEnum.Active);
        else result = !await _context.SeverityValues.AnyAsync(x => x.NumericValue == value && x.Id != id && x.ActiveStatusId == (long)ActiveStatusEnum.Active);
        return result;
    }
}