using SIMA.Application.Query.Contract.Features.BranchManagement.Brokers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BranchManagement.Brokers.Interfaces;

public interface IBrokerReadRepository : IQueryRepository
{
    Task<Result<IEnumerable<GetBrokerQueryResult>>> GetAll(GetAllBrokerQuery request);
    Task<IEnumerable<GetBrokerQueryResult>> GetAllByBrokerTypeId(long brokerTypeId);
    Task<GetBrokerQueryResult> GetById(long id);
}
