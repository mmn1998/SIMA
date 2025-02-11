using SIMA.Application.Query.Contract.Features.BCP.SolutionPeriorities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.SolutionPeriorities;

public interface ISolutionPeriorityQueryRepository : IQueryRepository
{
    Task<GetSolutionPeriorityQueryResult> GetById(GetSolutionPeriorityQuery request);
    Task<Result<IEnumerable<GetSolutionPeriorityQueryResult>>> GetAll(GetAllSolutionPerioritiesQuery request);
}