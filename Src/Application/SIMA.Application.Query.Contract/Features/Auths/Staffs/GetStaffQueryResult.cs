namespace SIMA.Application.Query.Contract.Features.Auths.Staffs;

public class GetStaffQueryResult
{
    public long Id { get; set; }
    public string? StaffNumber { get; set; }
    public string? FullName { get; set; }
    public string? CompanyName { get; set; }
    public string? DepartmantName { get; set; }
    public string ActiveStatus { get; set; }
    public long ActiveStatusId { get; set; }
}
