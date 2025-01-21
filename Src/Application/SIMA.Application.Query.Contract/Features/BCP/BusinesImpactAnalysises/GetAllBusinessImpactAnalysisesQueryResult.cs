using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.BCP.BusinesImpactAnalysises;

public class GetAllBusinessImpactAnalysisesQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
    public long ServiceId { get; set; }
    public string? ServiceName { get; set; }
    public long ImportanceDegreeId { get; set; }
    public string? ImportanceDegreeName { get; set; }
    public long ServicePriorityId { get; set; }
    public string? ServicePriorityName { get; set; }
    public long BackupPeriodId { get; set; }
    public string? BackupPeriodName { get; set; }
    public string? RestartReason { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public long? IssueId { get; set; }
    public string? IssueCode { get; set; }
    public long? WorkflowId { get; set; }
    public string? WorkflowName { get; set; }
    public long? CurrentStateId { get; set; }
    public string? CurrentStateName { get; set; }
    public long? CurrentStepId { get; set; }
    public string? CurrentStepName { get; set; }
    public string? IssueCreatedAt { get; set; }
    public string? IssueCreatedBy { get; set; }
}