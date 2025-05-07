namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Services;

public class GetServicePrerequisiteQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public long? ServiceCategoryId { get; set; }
    public string? ServiceCategoryName { get; set; }
    public string? ServiceCategoryCode { get; set; }
    public long? ServiceStatusId { get; set; }
    public string? ServiceStatusName { get; set; }
    public string? ServiceStatusCode { get; set; }
    public long? ServicePriorityId { get; set; }
    public string? ServicePriorityName { get; set; }
    public string? ServicePriorityCode { get; set; }
    public long? TechnicalSupervisorDepartmentId { get; set; }
    public string? TechnicalSupervisorDepartmentName { get; set; }
    public string? TechnicalSupervisorDepartmentCode { get; set; }
}
