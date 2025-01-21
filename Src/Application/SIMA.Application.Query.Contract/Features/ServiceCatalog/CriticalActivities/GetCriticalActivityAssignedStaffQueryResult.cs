namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.CriticalActivities;

public class GetCriticalActivityAssignedStaffQueryResult
{
    public long ResponsilbeTypeId { get; set; }
    public string? ResponsibleTypeName { get; set; }
    public long StaffId { get; set; }
    public string? StaffFullName { get; set; }
    public long? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyName { get; set; }
}
