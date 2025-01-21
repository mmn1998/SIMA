using SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.Contracts;

public interface IEvaluationCriteriaDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, EvaluationCriteriaId? id = null);
    Task<bool> IsUnique(RiskPossibilityId riskPossibilityId, RiskImpactId riskImpactId, RiskDegreeId riskDegreeId, EvaluationCriteriaId? id = null);
}