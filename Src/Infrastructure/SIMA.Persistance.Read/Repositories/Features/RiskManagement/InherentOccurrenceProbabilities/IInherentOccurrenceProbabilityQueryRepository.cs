using SIMA.Application.Query.Contract.Features.RiskManagement.InherentOccurrenceProbabilities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.InherentOccurrenceProbabilities;

public interface IInherentOccurrenceProbabilityQueryRepository : IQueryRepository
{
    Task<GetInherentOccurrenceProbabilityQueryResult> GetById(GetInherentOccurrenceProbabilityQuery request);
    Task<Result<IEnumerable<GetInherentOccurrenceProbabilityQueryResult>>> GetAll(GetAllInherentOccurrenceProbabilitiesQuery request);
}