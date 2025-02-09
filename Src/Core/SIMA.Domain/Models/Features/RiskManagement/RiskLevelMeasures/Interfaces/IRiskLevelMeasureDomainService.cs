using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskLevelMeasures.Interfaces;

public interface IRiskLevelMeasureDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
    Task<bool> IsUnique(RiskPossibilityId riskPossibilityId, RiskLevelId riskLevelId, RiskImpactId riskImpactId, RiskLevelMeasureId? id = null);
}
