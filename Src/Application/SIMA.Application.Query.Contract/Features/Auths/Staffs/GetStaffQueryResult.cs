namespace SIMA.Application.Query.Contract.Features.Auths.Staffs;

public class GetStaffQueryResult
{
    public long Id { get; set; }
    public string? StaffNumber { get; set; }
    public string? FullName { get; set; }
    public long PositionId { get; set; }
    public long ProfileId { get; set; }
    public string? PositionName { get; set; }
    public string? CompanyName { get; set; }
    public long? CompanyId { get; set; }
    public string? DepartmentName { get; set; }
    public long? DepartmentId { get; set; }
    public string? ActiveStatus { get; set; }
    public long ActiveStatusId { get; set; }
    public long? ManagerId { get; set; }
    public string? ManagerFullName { get; set; }
    public long? BranchId { get; set; }
    public string? BranchName { get; set; }
    public long? PositionLevelId { get; set; }
    public string? PositionLevelName { get; set; }
}
