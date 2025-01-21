namespace SIMA.Domain.Models.Features.IssueManagement.Issues.Args;

public class CreateIssueArg
{
    public long Id { get; set; }
    public long CurrentWorkflowId { get; set; }
    public string Code { get; set; }
    public string Summery { get; set; }
    public long CurrentStateId { get; set; }
    public long CurrenStepId { get; set; }
    public long MainAggregateId { get; set; }
    public long SourceId { get; set; }
    public long IssueTypeId { get; set; }
    public long IssuePriorityId { get; set; }
    public long IssueWeightCategoryd { get; set; }
    public int Weight { get; set; }
    public DateTime IssueDate { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public long CompanyId { get; set; }
    public long RequesterId { get; set; }

    public List<CreateIssueLinkArg> IssueLinks { get; set; }
    public List<CreateIssueDocumentArg> IssueDocument { get; set; }

}