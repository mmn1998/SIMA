using SIMA.Application.Query.Contract.Features.BCP.PlanResponsibilities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.PlanResponsibilities;

public interface IPlanResponsibilityQueryRepository : IQueryRepository
{
    Task<GetPlanResponsibilityQueryResult> GetById(GetPlanResponsibilityQuery request);
    Task<Result<IEnumerable<GetPlanResponsibilityQueryResult>>> GetAll(GetAllPlanResponsibilitiesQuery request);
}