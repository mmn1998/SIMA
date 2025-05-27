using SIMA.Application.Query.Contract.Features.BCP.BusinessContinuityPlans;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.BusinessContinuityPlans;

public interface IBusinessContinuityPlanQueryRepository : IQueryRepository
{
    Task<GetBusinessContinuityPlanQueryResult> GetById(GetBusinessContinuityPlanQuery request);
    Task<GetBusinessContinuityPlanQueryResult> GetByIdAndVersionNumber(GetBusinessContinuityPlanByVersionQuery request);
    Task<Result<IEnumerable<GetAllBusinessContinuityPlansQueryResult>>> GetAll(GetAllBusinessContinuityPlansQuery request);
    Task<IEnumerable<GetAllPlanVersioningsByPlanIdQueryResult>> GetPlanVersioningByPlanId(long planId);
    Task<IEnumerable<GetAllPlanVersioningsByPlanIdQueryResult>> GetPlanAssumptionByPlanId(long planId);
}
