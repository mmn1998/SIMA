using SIMA.Domain.Models.Features.RiskManagement.Severities.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.Severities.Contracts;

public interface ISeverityDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, SeverityId? id = null);
}