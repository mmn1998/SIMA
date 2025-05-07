using SIMA.Application.Query.Contract.Features.RiskManagement.CurrentOccurrenceProbabilities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.CurrentOccurrenceProbabilities;

public interface ICurrentOccurrenceProbabilityQueryRepository : IQueryRepository
{
    Task<GetCurrentOccurrenceProbabilityQueryResult> GetById(GetCurrentOccurrenceProbabilityQuery request);
    Task<Result<IEnumerable<GetCurrentOccurrenceProbabilityQueryResult>>> GetAll(GetAllCurrentOccurrenceProbabilitiesQuery request);
}