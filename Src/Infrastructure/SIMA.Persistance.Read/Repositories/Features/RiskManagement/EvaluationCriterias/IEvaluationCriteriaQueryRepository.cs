using SIMA.Application.Query.Contract.Features.RiskManagement.EvaluationCriterias;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.EvaluationCriterias;

public interface IEvaluationCriteriaQueryRepository : IQueryRepository
{
    Task<Result<IEnumerable<GetEvaluationCriteriaQueryResult>>> GetAll(GetAllEvaluationCriteriasQuery request);
    Task<GetEvaluationCriteriaQueryResult> GetById(long id);
}