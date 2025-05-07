using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.TimeMeasurements.Contracts;
using SIMA.Domain.Models.Features.Auths.TimeMeasurements.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Auths.TimeMeasurements;

public class TimeMeasurementDomainService : ITimeMeasurementDomainService
{
    private readonly SIMADBContext _context;

    public TimeMeasurementDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, TimeMeasurementId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.TimeMeasurements.AnyAsync(x => x.Code == code);
        else result = !await _context.TimeMeasurements.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}