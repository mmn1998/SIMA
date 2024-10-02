namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Services;

public class GetServiceAssignedStaffQueryResult
{
    public long ResponsibleTypeId { get; set; }
    public string? ResponsibleTypeName { get; set; }
    public long StaffId { get; set; }
    public string? StaffFullName { get; set; }
}