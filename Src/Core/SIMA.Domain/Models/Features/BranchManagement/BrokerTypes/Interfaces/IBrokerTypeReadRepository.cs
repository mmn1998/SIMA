using SIMA.Application.Query.Contract.Features.BranchManagement.BrokerTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Interfaces
{
    public interface IBrokerTypeReadRepository : IQueryRepository
    {
        Task<GetBrokerTypeQueryResult> GetById(long id);
        Task<Result<IEnumerable<GetBrokerTypeQueryResult>>> GetAll(GetAllBrokerTypesQuery request);
    }
}
