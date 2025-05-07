using SIMA.Domain.Models.Features.Auths.Permissions.Args;
using SIMA.Domain.Models.Features.Auths.Permissions.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.Permissions;

public interface IPermissionRepository : IRepository<Permission>
{
    Task<Permission> GetById(long id);
    /// <summary>
    /// its a helper method for our needs
    /// </summary>
    /// <returns></returns>
    Task<bool> AddAllPermissionsToAUser(long userId);
    /// <summary>
    /// its a helper method for our needs
    /// </summary>
    /// <returns></returns>
    Task<bool> AddAllPermissionsFromAUserToAnotherUser(long ownUserId, long targetUserId);
    Task<List<CreatePermissionArg>> AddPermissionNotExist();
}

