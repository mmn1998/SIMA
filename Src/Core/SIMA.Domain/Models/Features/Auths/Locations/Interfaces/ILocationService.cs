using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.Locations.Interfaces;

public interface ILocationService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
}
