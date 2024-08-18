using SIMA.Domain.Models.Features.Auths.UserTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.UserTypes.Interfaces;

public interface IUserTypeRepository : IRepository<UserType>
{
    Task<UserType> GetById(UserTypeId Id);
}