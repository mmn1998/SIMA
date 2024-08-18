using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.StrategyTypes.Contracts
{
    public interface IStrategyTypeDomianService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, StrategyTypeId? id = null);
    }
}
