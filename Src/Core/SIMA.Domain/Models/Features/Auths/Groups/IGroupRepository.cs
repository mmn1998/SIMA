using SIMA.Domain.Models.Features.Auths.Groups.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.Groups;

public interface IGroupRepository : IRepository<Group>
{
    Task<Group> GetById(long id);
}
