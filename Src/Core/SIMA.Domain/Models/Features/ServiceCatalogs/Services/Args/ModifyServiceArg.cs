namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;

public class ModifyServiceArg
{
    public long Id { get;  set; }
    public string? Name { get;  set; }
    public string? Code { get;  set; }
    public long? ParentId { get;  set; }
    public double ServiceCost { get;  set; }
    public string? Description { get;  set; }
    public string? ServiceWorkflowDescription { get;  set; }
    public string? ContinuousImprovement { get;  set; }
    public string? Suggestion { get;  set; }
    public string? FeedbackUrl { get;  set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long ModifiedBy { get; set; }
    public long? TechnicalSupervisorDepartmentId { get; set; }
    public long ServiceBoundleId { get; set; }
    public long? ServiceStatusId { get; set; }
    public DateOnly? InServiceDate { get; set; }
}
