using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.Contracts;

public interface IRecoveryPointObjectiveDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, RecoveryPointObjectiveId? id = null);
}