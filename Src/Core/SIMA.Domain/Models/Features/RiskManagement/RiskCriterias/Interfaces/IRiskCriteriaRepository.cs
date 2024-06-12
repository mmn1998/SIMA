using SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Interfaces;

public interface IRiskCriteriaRepository : IRepository<RiskCriteria>
{
    Task<RiskCriteria> GetById(long id);
}
