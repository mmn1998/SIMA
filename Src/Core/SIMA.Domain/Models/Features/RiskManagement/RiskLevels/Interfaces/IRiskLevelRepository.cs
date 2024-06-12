using SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Interfaces
{
    public interface IRiskLevelRepository : IRepository<RiskLevel>
    {
        Task<RiskLevel> GetById(long id);
    }
}
