using SIMA.Domain.Models.Features.Auths.PositionTypes.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.PositionTypes.Contracts;

public interface IPositionTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, PositionTypeId? id = null);
}