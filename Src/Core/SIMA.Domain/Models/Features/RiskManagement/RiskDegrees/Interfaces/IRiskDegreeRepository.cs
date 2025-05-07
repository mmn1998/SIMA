using SIMA.Domain.Models.Features.RiskManagement.RiskDegrees.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskDegrees.Interfaces
{
    public interface IRiskDegreeRepository : IRepository<RiskDegree>
    {
        Task<RiskDegree> GetById(long id);
    }
}
