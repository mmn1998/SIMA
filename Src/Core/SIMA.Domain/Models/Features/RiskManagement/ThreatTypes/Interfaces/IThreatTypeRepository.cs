using SIMA.Domain.Models.Features.IssueManagement.IssueTypes.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Interfaces
{
    public interface IThreatTypeRepository :IRepository<ThreatType>
    {
        Task<ThreatType> GetById(long id);
    }
}
