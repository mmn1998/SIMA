using SIMA.Domain.Models.Features.BCP.StrategyTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.StrategyTypes.Contracts
{
    public interface IStrategyTypeRepository : IRepository<StrategyType>
    {
        Task<StrategyType> GetById(StrategyTypeId id);
    }
}
