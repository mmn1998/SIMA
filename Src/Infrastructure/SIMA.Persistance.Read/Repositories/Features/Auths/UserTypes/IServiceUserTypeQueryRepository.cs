using SIMA.Application.Query.Contract.Features.Auths.UserTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.UserTypes;

public interface IServiceUserTypeQueryRepository : IQueryRepository
{
    Task<GetUserTypeQueryResult> GetById(GetUserTypeQuery request);
    Task<Result<IEnumerable<GetUserTypeQueryResult>>> GetAll(GetAllUserTypesQuery request);
}