using SIMA.Domain.Models.Features.Auths.Positions.Entities;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Interfaces;

public interface IResponsibleTypeRepository : IRepository<ResponsibleType>
{
    Task<ResponsibleType> GetById(long id);
}
