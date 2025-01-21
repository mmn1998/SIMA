namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;

public class CreateServiceArg
{
    public long Id { get;  set; }
    public string? Name { get;  set; }
    public string? Code { get;  set; }
    public long? ParentId { get;  set; }
    public decimal? ServiceCost { get;  set; }
    public string? Description { get;  set; }
    public string? ServiceWorkflowBpmn { get;  set; }
    public string? ContinuousImprovement { get;  set; }
    public string? FeedbackUrl { get;  set; }
    public long TechnicalSupervisorDepartmentId { get; set; }
    public long ServiceCategoryId { get; set; }
    public long ServicePriorityId { get; set; }
    public long ActiveStatusId { get; set; }
    public long? ServiceStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
    public string? IsInternalService { get; set; }
    public string? IsCriticalService { get; set; }
    public long IssueId { get; set; }
    public long IssueWeightCategoryId { get; set; }
    public long IssuePriorityId { get; set; }
}