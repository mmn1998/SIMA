using SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftOrigins;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.DraftOrigins;

public interface IDraftOriginQueryRepository : IQueryRepository
{
    Task<GetDraftOriginQueryResult> GetById(GetDraftOriginQuery request);
    Task<Result<IEnumerable<GetDraftOriginQueryResult>>> GetAll(GetAllDraftOriginsQuery request);
}