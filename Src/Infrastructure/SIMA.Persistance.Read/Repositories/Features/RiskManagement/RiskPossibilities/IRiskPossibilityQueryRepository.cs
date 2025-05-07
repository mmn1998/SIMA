using SIMA.Application.Query.Contract.Features.RiskManagement.RiskPossibilities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskPossibilities
{
    public interface IRiskPossibilityQueryRepository : IQueryRepository
    {
        Task<Result<IEnumerable<GetRiskPossibilitiesQueryResult>>> GetAll(GetAllRiskPossibilitiesQuery request);
        Task<GetRiskPossibilitiesQueryResult> GetById(long id);
    }
}
