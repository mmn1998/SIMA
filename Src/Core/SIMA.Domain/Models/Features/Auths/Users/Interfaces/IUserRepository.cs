using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.Events;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.Users.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetById(long id);
    Task<User> GetByUserName(string userName);
    Task<SSOInfoUserEvent> GetUserInfoWithSSO(string tiket);

}
