
using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.Severities.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.Severities.Contracts;

public interface ISeverityDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, SeverityId? id = null);
    Task<bool> IsRelationsUnique(AffectedHistoryId AffectedHistoryId,SeverityValueId SeverityValueId,ConsequenceLevelId ConsequenceLevelId, SeverityId? id = null);
}