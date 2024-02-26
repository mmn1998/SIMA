using SIMA.Application.Query.Contract.Features.BranchManagement.Brokers;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BranchManagement.Brokers.Interfaces;

public interface IBrokerReadRepository : IQueryRepository
{
    Task<List<GetBrokerQueryResult>> GetAll(BaseRequest baseRequest);
    Task<GetBrokerQueryResult> GetById(long id);
}
