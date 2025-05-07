using SIMA.Framework.Common.Helper;


namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.CriticalActivities;

public class GetAllCriticalActivitiesQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public long TechnicalSupervisorDepartmentId { get; set; }
    public string? TechnicalSupervisorDepartmentName { get; set; }
    public string? TechnicalSupervisorDepartmentCode { get; set; }
    public string? IssueDescription { get; set; }
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