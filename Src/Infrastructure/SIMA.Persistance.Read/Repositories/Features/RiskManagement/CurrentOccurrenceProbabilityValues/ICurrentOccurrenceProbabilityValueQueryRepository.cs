using SIMA.Application.Query.Contract.Features.RiskManagement.CurrentOccurrenceProbabilityValues;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.CurrentOccurrenceProbabilityValues;

public interface ICurrentOccurrenceProbabilityValueQueryRepository : IQueryRepository
{
    Task<GetCurrentOccurrenceProbabilityValueQueryResult> GetById(GetCurrentOccurrenceProbabilityValueQuery request);
    Task<Result<IEnumerable<GetCurrentOccurrenceProbabilityValueQueryResult>>> GetAll(GetAllCurrentOccurrenceProbabilityValuesQuery request);
}