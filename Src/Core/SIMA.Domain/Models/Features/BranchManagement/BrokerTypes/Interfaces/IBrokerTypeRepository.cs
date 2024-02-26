using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Interfaces
{
    public interface IBrokerTypeRepository : IRepository<BrokerType>
    {
        Task<BrokerType> GetById(long id);
    }
}
