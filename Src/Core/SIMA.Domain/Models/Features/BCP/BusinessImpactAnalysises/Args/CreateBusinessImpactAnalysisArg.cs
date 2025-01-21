namespace SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;

public class CreateBusinessImpactAnalysisArg
{
    public long Id { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public long ImportanceDegreeId { get; set; }
    public long ServicePriorityId { get; set; }
    public long BackupPeriodId { get; set; }
    public long ServiceId { get; set; }
    public string? RestartReason { get; set; }
    public long IssueWeightCategoryId { get; set; }
    public long IssuePriorityId { get; set; }
    public long IssueId { get; set; }
}