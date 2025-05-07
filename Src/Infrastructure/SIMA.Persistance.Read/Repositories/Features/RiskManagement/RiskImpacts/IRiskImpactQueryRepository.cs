using SIMA.Application.Query.Contract.Features.RiskManagement.RiskImpacts;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskImpacts
{
    public interface IRiskImpactQueryRepository : IQueryRepository
    {
        Task<Result<IEnumerable<GetRiskImpactsQueryResult>>> GetAll(GetAllRiskImpactsQuery request);
        Task<GetRiskImpactsQueryResult> GetById(long id);
    }
}
