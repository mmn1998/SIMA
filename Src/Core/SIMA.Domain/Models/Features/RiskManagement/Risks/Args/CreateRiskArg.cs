namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Args;

public class CreateRiskArg
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public long RiskTypeId { get; set; }
    public long ConsequenceLevelId { get; set; }
    public string? IsNeedCobit { get; set; }
    public long AffectedHistoryId { get; set; }
    public long UseVulnerabilityId { get; set; }
    public long ConsequenceCategoryId { get; set; }
    public long ScenarioHistoryId { get; set; }
    public long TriggerStatusId { get; set; }
    public long FrequencyId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
