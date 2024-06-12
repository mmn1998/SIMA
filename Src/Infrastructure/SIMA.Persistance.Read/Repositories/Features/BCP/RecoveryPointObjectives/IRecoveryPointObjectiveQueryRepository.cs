using SIMA.Application.Query.Contract.Features.BCP.RecoveryPointObjectives;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.Consequences;

public interface IRecoveryPointObjectiveQueryRepository : IQueryRepository
{
    Task<GetRecoveryPointObjectiveQueryResult> GetById(GetRecoveryPointObjectiveQuery request);
    Task<Result<IEnumerable<GetRecoveryPointObjectiveQueryResult>>> GetAll(GetAllRecoveryPointObjectivesQuery request);
}