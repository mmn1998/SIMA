using SIMA.Domain.Models.Features.Auths.Positions.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.Positions.Repositories;

public interface IPositionRepository : IRepository<Position>
{
    Task<Position> GetById(long id);
}
