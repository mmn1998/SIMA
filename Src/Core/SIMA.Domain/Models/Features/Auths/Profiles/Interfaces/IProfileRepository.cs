using SIMA.Domain.Models.Features.Auths.Profiles.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.Profiles.Interfaces;

public interface IProfileRepository : IRepository<Profile>
{
    Task<Profile> GetById(long id);
}
