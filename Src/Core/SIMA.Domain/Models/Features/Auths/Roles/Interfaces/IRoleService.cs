using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.Roles.Interfaces;

public interface IRoleService : IDomainService
{
    Task<bool> IsRoleSatisfied(string code, string englishKey, long id);
}
