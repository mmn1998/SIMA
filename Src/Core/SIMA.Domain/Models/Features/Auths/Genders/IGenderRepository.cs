using SIMA.Domain.Models.Features.Auths.Genders.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.Genders;

public interface IGenderRepository : IRepository<Gender>
{
    Task<Gender> GetById(long id);

}
