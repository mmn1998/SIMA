using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Staffs;

public class GetStaffQuery : IQuery<Result<GetStaffQueryResult>>
{
    public long Id { get; set; }
}
public class GetStaffByDepartmentQuery : IQuery<Result<IEnumerable<GetStaffQueryResult>>>
{
    public long? DepartmentId { get; set; }
}
