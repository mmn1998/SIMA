using SIMA.Domain.Models.Features.Auths.Roles.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.Roles.Interfaces;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role> GetById(long id);
}
