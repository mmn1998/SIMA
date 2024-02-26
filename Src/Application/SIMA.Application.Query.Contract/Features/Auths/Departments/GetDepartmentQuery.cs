using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Departments;

public class GetDepartmentQuery : IQuery<Result<GetDepartmentQueryResult>>
{
    public long Id { get; set; }
}
