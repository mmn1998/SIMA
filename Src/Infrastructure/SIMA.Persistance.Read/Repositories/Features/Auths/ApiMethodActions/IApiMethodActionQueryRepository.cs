using SIMA.Application.Query.Contract.Features.Auths.ApiMethodActions;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.ApiMethodActions;

public interface IApiMethodActionQueryRepository : IQueryRepository
{
    Task<IEnumerable<GetApiMethodActionQueryResult>> GetAll(GetAllApiMethodActionsQuery request);
}
