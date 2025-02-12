using SIMA.Application.Query.Contract.Features.BCP.SolutionPriorities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.SolutionPeriorities;

public interface ISolutionPriorityQueryRepository : IQueryRepository
{
    Task<GetSolutionPriorityQueryResult> GetById(GetSolutionPriorityQuery request);
    Task<Result<IEnumerable<GetSolutionPriorityQueryResult>>> GetAll(GetAllSolutionPrioritiesQuery request);
}