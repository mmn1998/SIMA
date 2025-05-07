using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.Permissions.Interfaces;

public interface IPermissionService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
}
