using SIMA.Application.Query.Contract.Features.BranchManagement.BrokerTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Interfaces
{
    public interface IBrokerTypeReadRepository : IQueryRepository
    {
        Task<GetBrokerTypeQueryResult> GetById(long id);
        Task<List<GetBrokerTypeQueryResult>> GetAll(BaseRequest request);
    }
}
