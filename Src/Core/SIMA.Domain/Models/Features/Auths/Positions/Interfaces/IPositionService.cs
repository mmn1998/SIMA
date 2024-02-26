using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.Positions.Interfaces;

public interface IPositionService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
}
