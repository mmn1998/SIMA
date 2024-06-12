using SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevelMeasures;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskLevelMeasures
{
    public interface IRiskLevelMeasureQueryRepository : IQueryRepository
    {
        Task<Result<IEnumerable<GetAllRiskLevelMeasuresQueryResult>>> GetAll(GetAllRiskLevelMeasuresQuery request);
        Task<GetRiskLevelMeasuresQueryResult> GetById(long id);
    }
}
