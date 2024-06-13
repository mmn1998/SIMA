using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.Entities;
using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.ValueObjects;
using SIMA.Framework.Core.Domain;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Logistics.UnitMeasurements.Contracts;

public interface IUnitMeasurementDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, UnitMeasurementId? id = null);
}
public interface IUnitMeasurementRepository: IRepository<UnitMeasurement>
{
    Task<UnitMeasurement> GetById(UnitMeasurementId id);
}