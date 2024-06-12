using SIMA.Application.Query.Contract.Features.RiskManagement.RiskCriterias;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskCriterias
{
    public interface IRiskCriteriaQueryRepository : IQueryRepository
    {
        Task<Result<IEnumerable<GetAllRiskCriteriasQueryResult>>> GetAll(GetAllRiskCriteriasQuery request);
        Task<GetRiskCriteriaQueryResult> GetById(long id);
    }
}
