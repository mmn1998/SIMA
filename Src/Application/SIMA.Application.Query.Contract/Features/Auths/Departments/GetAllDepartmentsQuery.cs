using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Departments;

public class GetAllDepartmentsQuery : BaseRequest, IQuery<Result<IEnumerable<GetDepartmentQueryResult>>>
{
}
