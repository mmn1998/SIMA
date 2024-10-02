namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.CriticalActivities;

public class GetCriticalActivityAssignedStaffQueryResult
{
    public long ResponsibleTypeId { get; set; }
    public string? ResponsibleTypeName { get; set; }
    public long StaffId { get; set; }
    public string? StaffFullName { get; set; }
}
