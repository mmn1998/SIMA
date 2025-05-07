using SIMA.Domain.Models.Features.Auths.TimeMeasurements.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.TimeMeasurements.Contracts;

public interface ITimeMeasurementDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, TimeMeasurementId? id = null);
}