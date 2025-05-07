using SIMA.Domain.Models.Features.Auths.PositionLevels.Entities;
using SIMA.Domain.Models.Features.Auths.PositionLevels.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.PositionLevels.Contracts;

public interface IPositionLevelRepository : IRepository<PositionLevel>
{
    Task<PositionLevel> GetById(PositionLevelId id);
}