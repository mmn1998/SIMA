using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;
using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Services;

public class GetServiceQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ServiceCategoryId { get; set; }
    public string? ServiceCategoryName { get; set; }
    public string? ServiceCategoryCode { get; set; }
    public long TechnicalSupervisorDepartmentId { get; set; }
    public string? TechnicalSupervisorDepartmentName { get; set; }
    public string? TechnicalSupervisorDepartmentCode { get; set; }
    public long? ServiceCategoryParentId { get; set; }
    public string? IsCriticalService { get; set; }
    public string? Description { get; set; }
    public DateOnly? InServiceDate { get; set; }
    public long? ServiceStatusId { get; set; }
    public string? ServiceStatusName { get; set; }
    public long? ServicePriorityId { get; set; }
    public string? ServicePriorityName { get; set; }
    public string? InServiceDatePersian => InServiceDate.ToPersianDate();
    public string? WorkflowFileContent { get; set; }
    public string? FeedbackUrl { get; set; }
    public string? ActiveStatus { get; set; }
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
    public IssueInfo? IssueInfo { get; set; }
    public IEnumerable<IssueApprovalList>? IssueApprovalList { get; set; }
    public IEnumerable<GetApprovalOptionQueryResult>? ApprovalOptionList { get; set; }
    public IEnumerable<GetRelatedProgressQueryResult>? RelatedProgressList { get; set; }
    public IEnumerable<GetStepRequiredDocumentQueryResult>? StepRequiredDocumentList { get; set; }
    public string? UiPropertyBoxTitle { get; set; }
    public IEnumerable<StoreProcedureParams>? FormParams { get; set; }
    public string? IsEditable { get; set; }
    public string? CreatedBy { get; set; }
}
