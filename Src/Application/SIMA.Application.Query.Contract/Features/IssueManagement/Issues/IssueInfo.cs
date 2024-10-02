using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues;

public class IssueInfo
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public long WorkflowId { get; set; }
    public string? HasDocument { get; set; }
    public string? WorkflowName { get; set; }
    public string? WorkFlowFileContent { get; set; }
    public long MainAggregateId { get; set; }
    public long IssuePriorityId { get; set; }
    public string? IssuePriorityName { get; set; }
    public long IssueWeightId { get; set; }
    public string? IssueWeightName { get; set; }
    public int Weight { get; set; }
    public long CurrentStateId { get; set; }
    public string? CurrentStateName { get; set; }
    public long CurrentStepId { get; set; }
    public string? CurrentStepName { get; set; }
    public string? StepDisplayName { get; set; }
    public string? CurrentStepBpmnId { get; set; }
    public DateTime? DueDate { get; set; }
    public string? DueDatePersian => DueDate.ToPersianDateTime();
    public IEnumerable<GetIssueCommentQueryResult>? IssueCommentList { get; set; }
}



