using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Staffs;

public class GetStaffByStaffNumberQuery : IQuery<Result<GetStaffByStaffNumberQueryResult>>
{
    public string StaffNumber { get; set; }
}
public class CompanyInfo
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}
public class BranchInfo
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}
public class DepartmentInfo
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}
