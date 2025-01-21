using Sima.Framework.Core.Mediator;
using SIMA.Application.Contract.Features.ServiceCatalog.Services;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.ServiceCatalog.CriticalActivities;

public class CreateCriticalActivityCommand : ICommand<Result<long>>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<long>? RelatedServiceList { get; set; }
    public List<long>? RiskList { get; set; }
    public List<long>? AssetList { get; set; }
    public List<long>? ConfigurationItemList { get; set; }
    public List<CreateServiceAssignedStaffCommand>? AssignedStaffList { get; set; }
    public string? IsFullTimeExecution { get; set; }
    public List<CreateCriticalActivityExecutionPlanCommand>? ExecutionPlanList { get; set; }
}