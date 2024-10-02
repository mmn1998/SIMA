using SIMA.Application.Query.Contract.Features.Auths.AccessTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.AccessTypes;

public interface IAccessTypeQueryRepository : IQueryRepository
{
    Task<GetAccessTypeQueryResult> GetById(GetAccessTypeQuery request);
    Task<Result<IEnumerable<GetAccessTypeQueryResult>>> GetAll(GetAllAccessTypesQuery request);
}
