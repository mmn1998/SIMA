using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.Risks;

public class GetAllRisksQuery : BaseRequest, IQuery<Result<IEnumerable<GetAllRisksQueryResult>>>
{
}
public class GetAllRisksQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? IsNeedCobit { get; set; }
    public long RiskTypeId { get; set; }
    public string? RiskTypeName { get; set; }
    public long AffectedHistoryId { get; set; }
    public string? AffectedHistoryName { get; set; }
    public long UseVulnerabilityId { get; set; }
    public string? UseVulnerabilityName { get; set; }
    public long ScenarioHistoryId { get; set; }
    public string? ScenarioHistoryName { get; set; }
    public long FrequencyId { get; set; }
    public string? FrequencyName { get; set; }
    public long TriggerStatusId { get; set; }
    public string? TriggerStatusName { get; set; }
    public long ConsequenceCategoryId { get; set; }
    public long? ConsequenceLevelId { get; set; }
    public string? ConsequenceLevelName { get; set; }
    public string? ConsequenceCategoryName { get; set; }
    public string? Description { get; set; }
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
public class GetRiskQuery : IQuery<Result<GetRiskQueryResult>>
{
    public long Id { get; set; }
}
public class GetRiskQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? IsNeedCobit { get; set; }
    public long RiskTypeId { get; set; }
    public string? RiskTypeName { get; set; }
    public long AffectedHistoryId { get; set; }
    public string? AffectedHistoryName { get; set; }
    public long UseVulnerabilityId { get; set; }
    public string? UseVulnerabilityName { get; set; }
    public long ScenarioHistoryId { get; set; }
    public string? ScenarioHistoryName { get; set; }
    public long FrequencyId { get; set; }
    public string? ConsequenceLevel { get; set; }
    public string? FrequencyName { get; set; }
    public long TriggerStatusId { get; set; }
    public string? TriggerStatusName { get; set; }
    public long ConsequenceCategoryId { get; set; }
    public string? ConsequenceCategoryName { get; set; }
    public string? Description { get; set; }
    public string? ActiveStatus { get; set; }
    public IEnumerable<GetCorrectiveActionQueryResult>? CorrectiveActionList { get; set; }
    public IEnumerable<GetPreventiveActionQueryResult>? PreventiveActionList { get; set; }
    public IEnumerable<GetEffectedAssetQueryResult>? EffectedAssetList { get; set; }
    public IEnumerable<GetServiceRiskImpactQueryResult>? ServiceRiskImpactList { get; set; }
    public IEnumerable<GetThreatQueryResult>? ThreatList { get; set; }
    public IEnumerable<GetServiceAssignedStaff>? ServiceAssignedStavesList { get; set; }
}
public class GetPreventiveActionQueryResult
{
    public long Id { get; set; }
    public string? ActionDescription { get; set; }
}
public class GetCorrectiveActionQueryResult
{
    public long Id { get; set; }
    public string? ActionDescription { get; set; }
}
public class GetEffectedAssetQueryResult
{
    public long Id { get; set; }
    public long AssetId { get; set; }
    public string? AssetName { get; set; }
    public string? AssetCode { get; set; }
    public decimal AV { get; set; }
    public float EF { get; set; }
    public float Sle { get; set; }
    public float Ale { get; set; }
    public string? Description { get; set; }
    public IEnumerable<GetVulnerabilityQueryResult>? VulnerabilityList { get; set; }
}
public class GetVulnerabilityQueryResult
{
    public long Id { get; set; }
    public long EffectedAssetId { get; set; }
    public string? Description { get; set; }
}
public class GetServiceRiskImpactQueryResult
{
    public long Id { get; set; }
    public long ServiceId { get; set; }
    public string? ServiceName { get; set; }
    public string? ServiceCode { get; set; }
    public IEnumerable<GetRiskImpactListQueryResult>? RiskImpactList { get; set; }
}
public class GetRiskImpactListQueryResult
{
    public long ServiceRiskId { get; set; }
    public long ImpactScaleId { get; set; }
    public string? ImpactScaleName { get; set; }
    public long RiskImpactId { get; set; }
    public string? RiskImpactName { get; set; }
}
public class GetThreatQueryResult
{
    public long ThreatTypeId { get; set; }
    public string? ThreatTypeName { get; set; }
    public long RiskPossibilityId { get; set; }
    public string? RiskPossibilityName { get; set; }
    public string? Description { get; set; }
}
public class GetServiceAssignedStaff
{
    public long serviceAssignedStaff { get; set; }
    public long staffId { get; set; }
}
