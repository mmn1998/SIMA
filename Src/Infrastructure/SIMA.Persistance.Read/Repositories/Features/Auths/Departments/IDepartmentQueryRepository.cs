using SIMA.Application.Query.Contract.Features.Auths.Departments;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Departments;

public interface IDepartmentQueryRepository : IQueryRepository
{
    Task<GetDepartmentQueryResult> FindById(long id);
    Task<Result<List<GetDepartmentQueryResult>>> GetAll(BaseRequest? baseRequest = null);
}
