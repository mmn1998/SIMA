using SIMA.Domain.Models.Features.BCP.RecoveryOptionPriorities.Entities;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.RecoveryOptionPriorities.Contracts
{
    public interface IRecoveryOptionPriorityDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, RecoveryOptionPriorityId? id = null);
    }
}
