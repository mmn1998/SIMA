using SIMA.Domain.Models.Features.Auths.TimeMeasurements.Entities;
using SIMA.Domain.Models.Features.Auths.TimeMeasurements.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.TimeMeasurements.Contracts;

public interface ITimeMeasurementRepository : IRepository<TimeMeasurement>
{
    Task<TimeMeasurement> GetById(TimeMeasurementId id);
}
