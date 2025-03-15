using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Auths.Staffs;

public class GetStaffByStaffNumberQueryResult
{
    public long Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? StaffNumber { get; set; }
    public string? ActiveStatus { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public CompanyInfo? CompanyInfo { get; set; }
    public BranchInfo? BranchInfo { get; set; }
    public DepartmentInfo? DepartmentInfo { get; set; }
}
