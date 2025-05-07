using SIMA.Application.Query.Contract.Features.RiskManagement.AffectedHistories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.AffectedHistories;

public interface IAffectedHistoryQueryRepository : IQueryRepository
{
    Task<GetAffectedHistoryQueryResult> GetById(GetAffectedHistoryQuery request);
    Task<Result<IEnumerable<GetAffectedHistoryQueryResult>>> GetAll(GetAllAffectedHistoriesQuery request);
}