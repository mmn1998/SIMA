using SIMA.Application.Query.Contract.Features.RiskManagement.EvaluationCriterias;
using SIMA.Application.Query.Contract.Features.RiskManagement.Frequencies;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.Frequencies;

public interface IFrequencyQueryRepository : IQueryRepository
{
    Task<Result<IEnumerable<GetFrequencyQueryResult>>> GetAll(GetAllFrequenciesQuery request);
    Task<GetFrequencyQueryResult> GetById(long id);
}