using SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Interfaces
{
    public interface IRiskImpactRepository : IRepository<RiskImpact>
    {
        Task<RiskImpact> GetById(long id);
    }
}
