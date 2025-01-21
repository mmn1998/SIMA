using SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftValorStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.DraftValorStatuses;

public interface IDraftValorStatusQueryRepository : IQueryRepository
{
    Task<GetDraftValorStatusQueryResult> GetById(GetDraftValorStatusQuery request);
    Task<Result<IEnumerable<GetDraftValorStatusQueryResult>>> GetAll(GetAllDraftValorStatusesQuery request);
}