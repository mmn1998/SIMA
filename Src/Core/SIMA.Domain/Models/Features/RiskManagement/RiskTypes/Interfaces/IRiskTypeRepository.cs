using SIMA.Domain.Models.Features.RiskManagement.RiskTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskTypes.Interfaces
{
    public interface IRiskTypeRepository : IRepository<RiskType>
    {
        Task<RiskType> GetById(long id);
    }
}
