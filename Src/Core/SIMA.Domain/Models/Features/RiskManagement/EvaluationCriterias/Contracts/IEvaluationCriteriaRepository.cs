using SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.Entities;
using SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.Contracts;

public interface IEvaluationCriteriaRepository : IRepository<EvaluationCriteria>
{
    Task<EvaluationCriteria> GetById(EvaluationCriteriaId id);
}
