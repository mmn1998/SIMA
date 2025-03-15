using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Services;

public class GetAllServicesQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ServiceCategoryId { get; set; }
    public string? IsInternalService { get; set; }
    public string? ServiceCategoryName { get; set; }
    public string? ServiceCategoryCode { get; set; }
    public long? ServiceTypeId { get; set; }
    public string? ServiceTypeName { get; set; }
    public long TechnicalSupervisorDepartmentId { get; set; }
    public string? TechnicalSupervisorDepartmentName { get; set; }
    public string? TechnicalSupervisorDepartmentCode { get; set; }
    public long? ServiceCategoryParentId { get; set; }
    public string? IsCriticalService { get; set; }
    public string? Description { get; set; }
    public long? ServiceStatusId { get; set; }
    public string? ServiceStatusName { get; set; }
    public long? ServicePriorityId { get; set; }
    public string? ServicePriorityName { get; set; }
    public string? WorkflowFileContent { get; set; }
    public string? FeedbackUrl { get; set; }
    public string? ActiveStatus { get; set; }
    public long IssueId { get; set; }
    public string? IssueCode { get; set; }
    public long WorkflowId { get; set; }
    public string? WorkflowCode { get; set; }
    public long MainAggregateId { get; set; }
    public long IssuePriorityId { get; set; }
    public string? IssuePriorityName { get; set; }
    public long IssueWeight { get; set; }
    public long IssueWeightId { get; set; }
    public string? IssueWeightName { get; set; }
    public long CurrentStateId { get; set; }
    public string? CurrentStateName { get; set; }
    public long CurrentStepId { get; set; }
    public string? CurrentStepName { get; set; }
    public string? HasDocument { get; set; }
    public DateTime? DueDate { get; set; }
    public string? DueDatePersian => DueDate.ToPersianDateTime();
    public string? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
}
public class GetServiceQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ServiceCategoryId { get; set; }
    public string? IsInternalService { get; set; }
    public string? ServiceDataFlowDiagram { get; set; }
    public string? ContinuousImprovement { get; set; }
    public string? ServiceCategoryName { get; set; }
    public string? ServiceCategoryCode { get; set; }
    public long TechnicalSupervisorDepartmentId { get; set; }
    public string? TechnicalSupervisorDepartmentName { get; set; }
    public string? CompanyName { get; set; }
    public long? CompanyId { get; set; }
    public string? TechnicalSupervisorDepartmentCode { get; set; }
    public long? ServiceCategoryParentId { get; set; }
    public string? IsCriticalService { get; set; }
    public string? Description { get; set; }
    public long? ServiceStatusId { get; set; }
    public string? ServiceStatusName { get; set; }
    public long? ServiceTypeId { get; set; }
    public string? ServiceTypeName { get; set; }
    public long? ServicePriorityId { get; set; }
    public string? ServicePriorityName { get; set; }
    public string? WorkflowFileContent { get; set; }
    public decimal? ServiceCost { get; set; }
    public string? FeedbackUrl { get; set; }
    public string? ActiveStatus { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public IEnumerable<GetServiceCustomerQueryResult>? ServiceCustomerList { get; set; }
    public IEnumerable<GetServiceUserQueryResult>? ServiceUserList { get; set; }
    public IEnumerable<GetServiceChannelQueryResult>? ServiceChannelList { get; set; }
    public IEnumerable<GetServiceProviderQueryResult>? ServiceProviderList { get; set; }
    public IEnumerable<GetServicePrerequisiteQueryResult>? ServicePrerequisiteList { get; set; }
    public IEnumerable<GetServiceRiskQueryResult>? ServiceRiskList { get; set; }
    public IEnumerable<GetServiceAssetQueryResult>? ServiceAssetList { get; set; }
    public IEnumerable<GetServiceConfigurationItemQueryResult>? ServiceConfigurationItemList { get; set; }
    public IEnumerable<GetServiceAssignedStaffQueryResult>? ServiceAssignedSttafList { get; set; }
    public IEnumerable<GetServiceProductQueryResult>? ServiceProductList { get; set; }
    public IEnumerable<GetServiceAvalibilityQueryResult>? ServiceAvalibilityList { get; set; }
    public IEnumerable<GetServiceOrganizationProjectQueryResult>? OrganizationProjectList { get; set; }
}
