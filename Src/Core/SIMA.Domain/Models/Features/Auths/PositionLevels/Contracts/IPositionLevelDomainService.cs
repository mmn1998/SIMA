using SIMA.Domain.Models.Features.Auths.PositionTypes.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.PositionLevels.Contracts;

public interface IPositionLevelDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, PositionLevelId? id = null);
}