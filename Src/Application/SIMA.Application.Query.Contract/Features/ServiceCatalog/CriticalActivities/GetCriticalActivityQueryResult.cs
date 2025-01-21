namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.CriticalActivities;

public class GetCriticalActivityQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long TechnicalSupervisorDepartmentId { get; set; }
    public string? TechnicalSupervisorDepartmentName { get; set; }
    public string? TechnicalSupervisorDepartmentCode { get; set; }
    public string? CreatedBy { get; set; }
    public string? Description { get; set; }
    public IEnumerable<GetCriticalActivityRelatedServiceResult>? RelatedServiceList { get; set; }
    public IEnumerable<GetCriticalActivityAssetQueryResult>? AssetList { get; set; }
    public IEnumerable<GetCriticalActivityConfigurationItemQueryResult>? ConfigurationItemList { get; set; }
    public IEnumerable<GetCriticalActivityRiskQueryResult>? RiskList { get; set; }
    public IEnumerable<GetCriticalActivityAssignedStaffQueryResult>? AssignedStaffList { get; set; }
    public IEnumerable<GetCriticalActivityExecutionPlansQueryResult>? ExecutionPlanList { get; set; }
}