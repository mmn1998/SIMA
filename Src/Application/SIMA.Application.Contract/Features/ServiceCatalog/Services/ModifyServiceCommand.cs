using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.ServiceCatalog.Services;

public class ModifyServiceCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ServiceBoundleId { get; set; }
    public string? IsCriticalService { get; set; }
    public string? Description { get; set; }
    public string? InServiceDate { get; set; }
    public long TechnicalSupervisorDepartmentId { get; set; }
    public string? WorkflowDesription { get; set; }
    public string? WorkflowFileContent { get; set; }
    public string? Suggestions { get; set; }
    public string? FeedbackUrl { get; set; }
    public List<long>? CustomerList { get; set; }
    public List<long>? UserTypeList { get; set; }
    public List<long>? ChannelList { get; set; }
    public List<long>? PrerequisiteServiceList { get; set; }
    public List<long>? ProviderList { get; set; }
    public List<long>? RiskList { get; set; }
    public List<long>? AssetList { get; set; }
    public List<long>? ConfigurationItemList { get; set; }
    public List<CreateServiceAssignedStaffCommand>? ServiceAssignedStaffList { get; set; }
    public long? ServiceStatusId { get; set; }
}