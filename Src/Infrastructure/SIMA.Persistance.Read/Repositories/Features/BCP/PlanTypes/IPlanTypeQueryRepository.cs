using SIMA.Application.Query.Contract.Features.BCP.PlanTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.PlanTypes;

public interface IPlanTypeQueryRepository : IQueryRepository
{
    Task<GetPlanTypeQueryResult> GetById(GetPlanTypeQuery request);
    Task<Result<IEnumerable<GetPlanTypeQueryResult>>> GetAll(GetAllPlanTypesQuery request);
}