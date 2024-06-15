using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.Entities;
using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Logistics.UnitMeasurements.Contracts;

public interface IUnitMeasurementRepository: IRepository<UnitMeasurement>
{
    Task<UnitMeasurement> GetById(UnitMeasurementId id);
}