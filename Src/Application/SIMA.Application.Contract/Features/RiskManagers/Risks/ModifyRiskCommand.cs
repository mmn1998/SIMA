using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.Risks;

public class ModifyRiskCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long AffectedHistoryId { get; set; }
    public long UseVulnerabilityId { get; set; }
    public string ConsequenceLevel { get; set; }
    public long ConsequenceCategoryId { get; set; }
    public long TriggerStatusId { get; set; }
    public long ScenarioHistoryId { get; set; }
    public long FrequencyId { get; set; }
    public string? IsNeedCobit { get; set; }
    public long RiskTypeId { get; set; }
    public string? Description { get; set; }
    public List<string>? PreventiveActionList { get; set; }
    public List<string>? CorrectiveActionList { get; set; }
    public List<CreateEffectedAssetCommand>? EffectedAssetList { get; set; }
    public List<string>? StaffList { get; set; }
    public List<CreateserviceRiskImpactCommand>? ServiceRiskImpactList { get; set; }
    public List<CreateThreatCommand>? ThreatList { get; set; }
    public List<CreateServiceAssignedStaff> ServiceAssignedStavesList { get; set; }
}
