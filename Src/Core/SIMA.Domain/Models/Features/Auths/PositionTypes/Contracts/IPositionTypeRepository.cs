using SIMA.Domain.Models.Features.Auths.PositionTypes.Entities;
using SIMA.Domain.Models.Features.Auths.PositionTypes.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.PositionTypes.Contracts;

public interface IPositionTypeRepository : IRepository<PositionType>
{
    Task<PositionType> GetById(PositionTypeId id);
}