using SIMA.Application.Query.Contract.Features.TrustyDrafts.RequestValors;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.RequestValors;

public interface IRequestValorQueryRepository : IQueryRepository
{
    Task<GetRequestValorQueryResult> GetById(GetRequestValorQuery request);
    Task<Result<IEnumerable<GetRequestValorQueryResult>>> GetAll(GetAllRequestValorsQuery request);
}