using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.Contracts;
using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Logistics.UnitMeasurements;

public class UnitMeasurementDomainService : IUnitMeasurementDomainService
{
    private readonly SIMADBContext _context;

    public UnitMeasurementDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, UnitMeasurementId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.UnitMeasurements.AnyAsync(x => x.Code == code);
        else result = !await _context.UnitMeasurements.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}