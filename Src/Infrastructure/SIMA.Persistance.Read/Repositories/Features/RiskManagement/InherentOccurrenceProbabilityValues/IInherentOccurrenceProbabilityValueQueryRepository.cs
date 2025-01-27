using SIMA.Application.Query.Contract.Features.RiskManagement.InherentOccurrenceProbabilityValues;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.InherentOccurrenceProbabilityValues;

public interface IInherentOccurrenceProbabilityValueQueryRepository : IQueryRepository
{
    Task<GetInherentOccurrenceProbabilityValueQueryResult> GetById(GetInherentOccurrenceProbabilityValueQuery request);
    Task<Result<IEnumerable<GetInherentOccurrenceProbabilityValueQueryResult>>> GetAll(GetAllInherentOccurrenceProbabilityValuesQuery request);
}