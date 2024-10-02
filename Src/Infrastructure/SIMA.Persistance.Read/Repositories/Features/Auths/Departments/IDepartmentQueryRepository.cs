using SIMA.Application.Query.Contract.Features.Auths.Departments;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Departments;

public interface IDepartmentQueryRepository : IQueryRepository
{
    Task<GetDepartmentQueryResult> FindById(long id);
    Task<Result<IEnumerable<GetDepartmentQueryResult>>> GetAll(GetAllDepartmentsQuery? request = null);
    Task<Result<IEnumerable<GetDepartmentQueryResult>>> GetByCpmpamyId(long comapnyId);
}
