using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.LocationTypes.Interfaces;

public interface ILocationTypeService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
}
