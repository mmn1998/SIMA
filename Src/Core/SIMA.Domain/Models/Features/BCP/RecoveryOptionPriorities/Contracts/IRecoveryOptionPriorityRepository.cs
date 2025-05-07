using SIMA.Domain.Models.Features.BCP.RecoveryOptionPriorities.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.RecoveryOptionPriorities.Contracts
{
    public interface IRecoveryOptionPriorityRepository : IRepository<RecoveryOptionPriority>
    {
        Task<RecoveryOptionPriority> GetById(RecoveryOptionPriorityId id);
    }
}
