using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Services;

public class GetServiceQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ServiceBoundleId { get; set; }
    public string? IsCriticalService { get; set; }
    public string? Description { get; set; }
    public DateOnly? InServiceDate { get; set; }
    public long? ServiceStatusId { get; set; }
    public string? InServiceDatePersian => InServiceDate.ToPersianDate();
    public long TechnicalSupervisorDepartmentId { get; set; }
    public string? WorkflowDesription { get; set; }
    public string? WorkflowFileContent { get; set; }
    public string? Suggestions { get; set; }
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
}
