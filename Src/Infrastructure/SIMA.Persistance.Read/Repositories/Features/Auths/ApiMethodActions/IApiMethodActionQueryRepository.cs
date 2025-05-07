using SIMA.Application.Query.Contract.Features.Auths.ApiMethodActions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.ApiMethodActions;

public interface IApiMethodActionQueryRepository : IQueryRepository
{
    Task<GetApiMethodActionQueryResult> GetById(GetApiMethodActionQuery request);
    Task<Result<IEnumerable<GetApiMethodActionQueryResult>>> GetAll(GetAllApiMethodActionsQuery request);
}