using SIMA.Application.Query.Contract.Features.RiskManagement.Risks;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.Risks;

public interface IRiskQueryRepository : IQueryRepository
{
    Task<GetRiskQueryResult> GetById(long id);
    Task<Result<IEnumerable<GetAllRisksQueryResult>>> GetAll(GetAllRisksQuery request);
}
