using SIMA.Application.Query.Contract.Features.BCP.RecoveryOptionPriorities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.RecoveryOptionPriorities;

public interface IRecoveryOptionPriorityQueryRepository : IQueryRepository
{
    Task<GetRecoveryOptionPriorityQueryResult> GetById(GetRecoveryOptionPriorityQuery request);
    Task<Result<IEnumerable<GetRecoveryOptionPriorityQueryResult>>> GetAll(GetAllRecoveryOptionPrioritiesQuery request);
}