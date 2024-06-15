using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Logistics.UnitMeasurements.Contracts;

public interface IUnitMeasurementDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, UnitMeasurementId? id = null);
}