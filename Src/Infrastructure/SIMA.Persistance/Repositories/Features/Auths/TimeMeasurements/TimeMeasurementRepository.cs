using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.TimeMeasurements.Contracts;
using SIMA.Domain.Models.Features.Auths.TimeMeasurements.Entities;
using SIMA.Domain.Models.Features.Auths.TimeMeasurements.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Auths.TimeMeasurements;

public class TimeMeasurementRepository : Repository<TimeMeasurement>, ITimeMeasurementRepository
{
    private readonly SIMADBContext _context;

    public TimeMeasurementRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<TimeMeasurement> GetById(TimeMeasurementId Id)
    {
        var entity = await _context.TimeMeasurements.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}