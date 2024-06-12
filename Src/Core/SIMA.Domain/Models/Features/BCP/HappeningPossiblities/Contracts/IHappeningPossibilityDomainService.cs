using SIMA.Domain.Models.Features.BCP.HappeningPossiblities.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.HappeningPossiblities.Contracts;

public interface IHappeningPossibilityDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, HappeningPossibilityId? id = null);
}