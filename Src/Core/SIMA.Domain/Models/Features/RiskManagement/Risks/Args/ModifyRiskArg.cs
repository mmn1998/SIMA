namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Args;

public class ModifyRiskArg
{
    public long IssueId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string? IsNeedCobit { get; set; }
    public string Description { get; set; }
    public long RiskCategoryId { get; set; }
    public long ConsequenceLevelId { get; set; }
    public long AffectedHistoryId { get; set; }
    public long UseVulnerabilityId { get; set; }
    public long ScenarioHistoryId { get; set; }
    public long TriggerStatusId { get; set; }
    public long FrequencyId { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
