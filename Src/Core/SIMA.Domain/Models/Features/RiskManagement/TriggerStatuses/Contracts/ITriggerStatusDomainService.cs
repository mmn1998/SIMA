using SIMA.Domain.Models.Features.RiskManagement.TriggerStatuses.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.TriggerStatuses.Contracts;

public interface ITriggerStatusDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, TriggerStatusId? id = null);
    Task<bool> IsNumericUnique(float value, TriggerStatusId? id = null);
}