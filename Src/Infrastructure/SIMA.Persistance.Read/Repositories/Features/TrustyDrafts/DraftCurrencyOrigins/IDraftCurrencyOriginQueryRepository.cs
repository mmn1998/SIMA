using SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftCurrencyOrigins;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.DraftCurrencyOrigins;

public interface IDraftCurrencyOriginQueryRepository : IQueryRepository
{
    Task<GetDraftCurrencyOriginQueryResult> GetById(GetDraftCurrencyOriginQuery request);
    Task<Result<IEnumerable<GetDraftCurrencyOriginQueryResult>>> GetAll(GetAllDraftCurrencyOriginsQuery request);
}