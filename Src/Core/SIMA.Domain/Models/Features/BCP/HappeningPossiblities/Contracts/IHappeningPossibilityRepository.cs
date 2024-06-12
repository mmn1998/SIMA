using SIMA.Domain.Models.Features.BCP.HappeningPossiblities.Entities;
using SIMA.Domain.Models.Features.BCP.HappeningPossiblities.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.HappeningPossiblities.Contracts;

public interface IHappeningPossibilityRepository : IRepository<HappeningPossibility>
{
    Task<HappeningPossibility> GetById(HappeningPossibilityId id);
}
