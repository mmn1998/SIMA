using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.Contracts;
using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.Entities;
using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Logistics.UnitMeasurements;

public class UnitMeasurementRepository : Repository<UnitMeasurement>, IUnitMeasurementRepository
{
    private readonly SIMADBContext _context;

    public UnitMeasurementRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<UnitMeasurement> GetById(UnitMeasurementId Id)
    {
        var entity = await _context.UnitMeasurements.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}