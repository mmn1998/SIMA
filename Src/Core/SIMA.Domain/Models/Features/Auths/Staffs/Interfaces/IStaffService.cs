using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.Staffs.Interfaces;

public interface IStaffService : IDomainService
{
    Task<bool> IsStaffSatisfied(long profileId, long positionId);
    Task<bool> IsPositionDuplicated(long positionId, long profileId);
}
