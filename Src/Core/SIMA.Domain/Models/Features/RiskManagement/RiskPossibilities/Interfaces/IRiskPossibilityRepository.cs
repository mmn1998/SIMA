using SIMA.Domain.Models.Features.RiskManagement.RiskPossibilities.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskPossibilities.Interfaces
{
    public interface IRiskPossibilityRepository : IRepository<RiskPossibility>
    {
        Task<RiskPossibility> GetById(long id);
    }
}
